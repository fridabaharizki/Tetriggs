using System.Collections;
using UnityEngine;

public class Group : MonoBehaviour {

    float lastFall = 0;

	private float continuousVerticalSpeed = 0.05f;
	private float continuousHorizontalSpeed = 0.1f;

	private float verticalTimer = 0;
	private float horizontalTimer = 0;

	private int touchSensitivityHorizontal = 4;
	private int touchSensitivityVertical = 8;

	Vector2 previousUnitPosition = Vector2.zero;
	Vector2 direction = Vector2.zero;

	bool moved = false;

    void Start() {
        if (!isValidGridPos())
        {
			Application.LoadLevel ("Game_Over");
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    void Update() {

		if (Input.touchCount > 0)
		{
			Touch t = Input.GetTouch (0);
			if (t.phase == TouchPhase.Began) 
			{
				previousUnitPosition = new Vector2 (t.position.x, t.position.y);
			} else if (t.phase == TouchPhase.Moved) 
			{
				Vector2 touchDeltaPosition = t.deltaPosition;
				direction = touchDeltaPosition.normalized;

				if (Mathf.Abs (t.position.x - previousUnitPosition.x) >= touchSensitivityHorizontal && direction.x < 0 && t.deltaPosition.y > -10 && t.deltaPosition.y < 10)
				{
					MoveLeft ();
					previousUnitPosition = t.position;
					moved = true;
				} else if (Mathf.Abs (t.position.x - previousUnitPosition.x) >= touchSensitivityHorizontal && direction.x > 0 && t.deltaPosition.y > -10 && t.deltaPosition.x < 10) 
				{
					MoveRight ();
					previousUnitPosition = t.position;
					moved = true;
				} else if (Mathf.Abs (t.position.y - previousUnitPosition.y) >= touchSensitivityVertical && direction.y < 0 && t.deltaPosition.x > -10 && t.deltaPosition.x < 10)
				{
					MoveDown ();
					previousUnitPosition = t.position;
					moved = true;
				}

			} else if (t.phase == TouchPhase.Ended)
			{
				if (!moved && t.position.x > Screen.width / 4)
				{
					Rotate ();
				}
				moved = false;
			}
		}

		if (Time.time - lastFall >= 1)
		{
			MoveDown ();
		}
    }

	void MoveLeft()
	{
		if (horizontalTimer < continuousHorizontalSpeed)
		{
			horizontalTimer += Time.deltaTime;
			return;
		}
		horizontalTimer = 0;

		transform.position += new Vector3(-1, 0, 0);

		if (isValidGridPos())
			updateGrid();
		else
			transform.position += new Vector3(1, 0, 0);
	}

	void MoveRight()
	{
		if (horizontalTimer < continuousHorizontalSpeed)
		{
			horizontalTimer += Time.deltaTime;
			return;
		}
		horizontalTimer = 0;

		transform.position += new Vector3(1, 0, 0);

		if (isValidGridPos())
			updateGrid();
		else
			transform.position += new Vector3(-1, 0, 0);
	}

	void MoveDown()
	{
		if (verticalTimer < continuousVerticalSpeed)
		{
			verticalTimer += Time.deltaTime;
			return;
		}
		verticalTimer = 0;

		transform.position += new Vector3(0, -1, 0);

		if (isValidGridPos())
		{
			updateGrid();
		}
		else
		{
			transform.position += new Vector3(0, 1, 0);

			Grid.deleteFullRows();

			FindObjectOfType<Spawner>().spawnNext();

			enabled = false;
		}

		lastFall = Time.time;
	}

	void Rotate()
	{
		transform.Rotate(0, 0, -90);

		// See if valid
		if (isValidGridPos())
			updateGrid();
		else
			transform.Rotate(0, 0, 90);
	}

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            if (!Grid.insideBorder(v))
                return false;

            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
    void updateGrid()
    {
        for (int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}

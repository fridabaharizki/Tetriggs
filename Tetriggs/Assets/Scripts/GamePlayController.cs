using System.Collections;
using UnityEngine;


public class GamePlayController : MonoBehaviour {
	[SerializeField]
	private GameObject pausePanel;

	// Use this for initialization
	void Awake () {
		pausePanel.SetActive (false);
	}
	
	public void PauseGame()
	{
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
	}

	public void ResumeGame()
	{
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}

	public void QuitGame()
	{
		Time.timeScale = 1f;
		Application.LoadLevel ("Main_Menu");
	}
}

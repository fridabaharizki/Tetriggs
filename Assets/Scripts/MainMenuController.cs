using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
	public void PlayGame()
	{
		Application.LoadLevel("Scene_Main");
	}

	public void AboutUs()
	{
		Application.LoadLevel("About_Us");
	}

	public void HowToPlay()
	{
		Application.LoadLevel("How_To_Play");
	}

	public void Exit()
	{
		Application.Quit ();
	}
}

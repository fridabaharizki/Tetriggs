using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainController2 : MonoBehaviour {
	public void BackToMainMenu()
	{
		Application.LoadLevel("Main_Menu");
	}
}

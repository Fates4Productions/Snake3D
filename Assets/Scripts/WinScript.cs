﻿using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour
{
	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width / 3.2f, Screen.height / 2, Screen.width / 8, Screen.height / 10), "New Game"))
		{
			Application.LoadLevel(1);
		}
		if (GUI.Button(new Rect(Screen.width / 1.7f, Screen.height / 2, Screen.width / 8, Screen.height / 10), "Quit Game"))
		{
			Application.Quit();
		}
	}
}

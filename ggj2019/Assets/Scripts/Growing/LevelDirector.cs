using Sjabloon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : Singleton<LevelDirector>
{
	public delegate void VoidDelegate();

	public event VoidDelegate LevelStartEvent;
	public event VoidDelegate LevelResetEvent;

	//This is the last start function that get's fired in a scene
	private void Start()
	{
		if (LevelStartEvent != null)
			LevelStartEvent();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			ResetLevel();
	}

	private void ResetLevel()
	{
		if (LevelResetEvent != null)
			LevelResetEvent();
	}
}

﻿using Sjabloon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : Singleton<LevelDirector>
{
    public delegate void VoidDelegate();

    public event VoidDelegate LevelStartEvent;
    public event VoidDelegate LevelStopEvent;
    public event VoidDelegate LevelUpdateEvent;
	public event VoidDelegate LevelSucces;
	public event VoidDelegate LevelFailed; 
    //public event VoidDelegate LevelFixedUpdateEvent;

    private bool m_IsLevelStarted = false;
    public bool IsLevelStarted
    {
        get { return m_IsLevelStarted; }
    }

    //This is the last start function that get's fired in a scene
    private void Update()
    {
        if (m_IsLevelStarted == false)
            return;

        if (LevelUpdateEvent != null)
            LevelUpdateEvent();

        if (Input.GetKeyDown(KeyCode.R))
            CompleteLevel(false);
    }

    private void FixedUpdate()
    {
        //This unfortunatly doesn't work as FixedUpdate doesn't get called
        //if (m_IsLevelStarted == false)
        //    return;

        //if (LevelUpdateEvent != null)
        //    LevelUpdateEvent();
    }

    public void StartLevel()
    {
        if (LevelStartEvent != null)
            LevelStartEvent();

        m_IsLevelStarted = true;
    }

    public void StopLevel()
    {
        if (LevelStopEvent != null)
            LevelStopEvent();

        m_IsLevelStarted = false;
    }

	public void CompleteLevel(bool succes)
	{
		StopLevel();

		if (succes) LevelSucces?.Invoke(); 
		else LevelFailed?.Invoke();
	}
}

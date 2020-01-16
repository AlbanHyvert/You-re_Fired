﻿using Engine.Singleton;
using System;
using UnityEngine;

public class GameLoopManager : Singleton<GameLoopManager>
{
    #region Fields
    private bool _isPaused = false;
    public bool IsPaused { get { return _isPaused; } set { _isPaused = value; } }

    private event Action _getPlayer = null;
    public event Action GetPlayer
    {
        add
        {
            _getPlayer -= value;
            _getPlayer += value;
        }
        remove
        {
            _getPlayer -= value;
        }
    }

    private event Action _getInput = null;
    public event Action GetInput
    {
        add
        {
            _getInput -= value;
            _getInput += value;
        }
        remove
        {
            _getInput -= value;
        }
    }

    private event Action _getCamera = null;
    public event Action GetCamera
    {
        add
        {
            _getCamera -= value;
            _getCamera += value;
        }
        remove
        {
            _getCamera -= value;
        }
    }

    private event Action _getHeadBobbing = null;
    public event Action GetHeadBobbing
    {
        add
        {
            _getHeadBobbing -= value;
            _getHeadBobbing += value;
        }
        remove
        {
            _getHeadBobbing -= value;
        }
    }

    private event Action _getInteractions = null;
    public event Action GetInteractions
    {
        add
        {
            _getInteractions -= value;
            _getInteractions += value;
        }
        remove
        {
            _getInteractions -= value;
        }
    }

    private event Action _getCanvas = null;
    public event Action GetCanvas
    {
        add
        {
            _getCanvas -= value;
            _getCanvas += value;
        }
        remove
        {
            _getCanvas -= value;
        }
    }
    #endregion Fields

    private void Start()
    {
        _isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            _isPaused = !_isPaused;
        }

        if(_getCanvas != null)
        {
            _getCanvas();
        }

        if (_isPaused == false)
        {
            if (_getPlayer != null)
            {
                _getPlayer();
            }

            if (_getCamera != null)
            {
                _getCamera();
            }

            if (_getHeadBobbing != null)
            {
                _getHeadBobbing();
            }

            if (_getInteractions != null)
            {
                _getInteractions();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isPaused == false)
        {
            if (_getInput != null)
            {
                _getInput();
            }
        }
    }

    protected override void OnDestroy()
    {
        _getPlayer = null;
        _getInput = null;
        _getCamera = null;
        _getInteractions = null;
        _getHeadBobbing = null;
        _getCanvas = null;
        _isPaused = false;
    }
}

﻿using UnityEngine;
using System.Collections.Generic;
using Engine.Timer;
using Engine.Singleton;
using System;

public class Scoring : Singleton<Scoring>
{
    private List<GameObject> _files = null;

    private int _globalGain = 0;
    public int GlobalGain { get { return _globalGain; } set { _globalGain = value; } }

    [SerializeField] private int _timeMinutes = 5;
    public int TimeMinutes { get { return _timeMinutes; } set { _timeMinutes = value; } }

    [SerializeField] private int _timeSecond = 60;
    public int TimeSeconds { get { return _timeSecond; } set { _timeSecond = value; } }

    [SerializeField] private float _gainTimer = 1.1f;
    [SerializeField] private float _globalTimer = 60f;

    private Timer _gainTime = null;
    private Timer _globalTime = null;

    private event Action<int, int> _minutesSeconds = null;
    public event Action<int, int> MinutesSeconds
    {
        add
        {
            _minutesSeconds -= value;
            _minutesSeconds += value;
        }
        remove
        {
            _minutesSeconds -= value;
        }
    }

    private void Start()
    {
        _gainTime = new Timer();
        _globalTime = new Timer();

        _gainTime.ResetTimer(_gainTimer);
        _globalTime.ResetTimer(_globalTimer);

        GameLoopManager.Instance.GetPlayer += OnUpdate;
    }

    private void OnUpdate()
    {
        if (_minutesSeconds != null && GameManager.Instance.CurrentState == GameManager.GameState.GAME)
        {
            if (_timeMinutes >= 0 && _timeSecond >= 0)
            {
                if (_timeSecond <= 0)
                {
                    _timeMinutes--;
                    _timeSecond = 60;
                }

                if (_globalTime.TimeLeft >= 0)
                {
                    _globalTime.ReduceRemainingTime(1f);
                }
                else
                {
                    _globalTime.ResetTimer(_globalTimer);
                    _timeSecond--;
                }

                if (_gainTime.TimeLeft >= 0)
                {
                    _gainTime.ReduceRemainingTime(0.01f);
                }
                else
                {
                    _gainTime.ResetTimer(_gainTimer);
                    PlayerManager.Instance.Money += (int)(_globalGain / 30);
                }
            }
            else
            {
                GameLoopManager.Instance.IsPaused = true;
            }
            Debug.Log(_globalTime.TimeLeft);
            _minutesSeconds(_timeMinutes, _timeSecond);
        }
    }

    protected override void OnDestroy()
    {
        GameLoopManager.Instance.GetPlayer -= OnUpdate;
        _minutesSeconds = null;
    }
}
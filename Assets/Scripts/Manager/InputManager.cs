using Engine.Singleton;
using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private Vector3 _direction = Vector3.zero;

    private event Action _interact = null;
    public event Action Interact
    {
        add
        {
            _interact -= value;
            _interact += value;
        }
        remove
        {
            _interact -= value;
        }
    }

    private event Action _shuffle = null;
    public event Action Shuffle
    {
        add
        {
            _shuffle -= value;
            _shuffle += value;
        }
        remove
        {
            _shuffle -= value;
        }
    }

    private void Start()
    {
        GameLoopManager.Instance.GetInput += OnUpdate;
    }

    private void OnUpdate()
    {
        if(Input.GetKeyDown(InputFieldManager.Instance.Shuffle.Normalize()))
        {
            _shuffle();
        }

        if(_interact != null && Input.GetMouseButtonDown(0))
        {
            _interact();
        }
    }

    protected override void OnDestroy()
    {
        GameLoopManager.Instance.GetInput -= OnUpdate;
        _interact = null;
        _shuffle = null;
        _direction = Vector3.zero;
    }
}

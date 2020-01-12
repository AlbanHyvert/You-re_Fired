using UnityEngine;
using Engine.Singleton;
using System;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager>
{
    #region Enum
    public enum PlayerState
    {
        ALIVE,
        DEAD
    }
    #endregion Enum

    #region Fields
    [SerializeField] private GameObject _playerPrefab = null;

    [SerializeField] private bool _canPlayerInteract = true;
    public bool CanPlayerInteract { get { return _canPlayerInteract; } }

    private GameObject _player = null;
    public GameObject Player { get { return _player; } }

    private int _hp = 100;
    public int Hp { get { return _hp; } set { _hp = value; } }

    private event Action<int> _updateHp = null;
    public event Action<int> UpdateHp
    {
        add
        {
            _updateHp -= value;
            _updateHp += value;
        }
        remove
        {
            _updateHp -= value;
        }
    }

    #endregion Fields

    #region Methods
    public void InstantiatePlayer(Vector3 position, Quaternion rotation)
    {
        _player = Instantiate(_playerPrefab, position, rotation);
    }

    private void Start()
    {
        if (_playerPrefab == null)
        {
            throw new System.Exception("PlayerManager is trying to access a missing component." + _playerPrefab + "Exiting.");
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _updateHp = null;
    }
    #endregion Methods
}

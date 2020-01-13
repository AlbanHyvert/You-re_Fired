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
    [Header("Player")]
    [SerializeField] private GameObject _playerPrefab = null;
    [Header("Interactions Controller")]
    [SerializeField] private InteractionsController _interactionController = null;
    public InteractionsController Interactions { get { return _interactionController; } set { _interactionController = value; } }

    [Header("Can player Interact")]
    [SerializeField] private bool _canPlayerInteract = true;
    public bool CanPlayerInteract { get { return _canPlayerInteract; } }

    private GameObject _player = null;
    public GameObject Player { get { return _player; } }

    [Header("Player Hp")]
    [SerializeField] private int _hp = 100;
    public int Hp { get { return _hp; } set { _hp = value; } }

    [Header("Player Money")]
    [SerializeField] private int _money = 200;
    public int Money { get { return _money; } set { _money = value; } }

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

    private event Action<int> _updateMoney = null;
    public event Action<int> UpdateMoney
    {
        add
        {
            _updateMoney -= value;
            _updateMoney += value;
        }
        remove
        {
            _updateMoney -= value;
        }
    }
    #endregion Fields

    #region Methods
    private void Start()
    {
        if (_playerPrefab == null)
        {
            throw new System.Exception("PlayerManager is trying to access a missing component." + _playerPrefab + "Exiting.");
        }

        GameLoopManager.Instance.GetPlayer += OnUpdate;
    }

    public void InstantiatePlayer(Vector3 position, Quaternion rotation)
    {
        _player = Instantiate(_playerPrefab, position, rotation);
    }

    private void OnUpdate()
    {
        if(_updateHp != null)
        {
            _updateHp(_hp);
        }

        if(_updateMoney != null)
        {
            _updateMoney(_money);
        }
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        _updateHp = null;
        _updateMoney = null;
        GameLoopManager.Instance.GetPlayer -= OnUpdate;
    }
    #endregion Methods
}

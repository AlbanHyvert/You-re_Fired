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

    [Header("Can player Interact")]
    [SerializeField] private bool _canPlayerInteract = true;

    private GameObject _player = null;

    [Header("Player Money")]
    [SerializeField] private int _money = 200;
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

    #region Properties
    public bool CanPlayerInteract { get { return _canPlayerInteract; } }
    public int Money { get { return _money; } set { _money = value; } }
    public GameObject Player { get { return _player; } }
    public InteractionsController Interactions { get { return _interactionController; } set { _interactionController = value; } }
    #endregion Properties

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
        if(_updateMoney != null)
        {
            _updateMoney(_money);
        }
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        _updateMoney = null;
        GameLoopManager.Instance.GetPlayer -= OnUpdate;
    }
    #endregion Methods
}

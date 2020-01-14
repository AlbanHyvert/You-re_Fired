using UnityEngine;
using System.Collections.Generic;
using Engine.Timer;
using Engine.Singleton;

public class Scoring : Singleton<Scoring>
{
    private List<GameObject> _files = null;
    private int _globalGain = 0;
    public int GlobalGain { get { return _globalGain; } set { _globalGain = value; } }

    [SerializeField] private float _timer = 1.1f;
    private Timer _time = null;

    private void Start()
    {
        GameLoopManager.Instance.GetPlayer += OnUpdate;
    }

    private void OnUpdate()
    {
        PlayerManager.Instance.Money += (int)(_globalGain / 60);
    }

    protected override void OnDestroy()
    {
        GameLoopManager.Instance.GetPlayer -= OnUpdate;
    }
}


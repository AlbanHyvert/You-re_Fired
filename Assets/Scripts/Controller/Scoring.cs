using UnityEngine;
using Engine.Timer;
using System.Collections.Generic;

public class Scoring : MonoBehaviour
{
    private List<CandidateFile> _files = null;
    private int _globalGain = 0;
    [SerializeField] private float _timer = 1.1f;
    private Timer time;

    private void Start()
    {
        _files = new List<CandidateFile>();
        time = new Timer();
        time.ReduceRemainingTime(_timer);
    }
}

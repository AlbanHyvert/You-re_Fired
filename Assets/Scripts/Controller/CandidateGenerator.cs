﻿using UnityEngine;

public class CandidateGenerator : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform[] _instantiateSpawner = null;
    [SerializeField] private Transform _candidateParent = null;
    private GameObject[] _files = null;
    private GameObject _candidate = null;
    #endregion Fields

    #region Methods
    private void Start()
    {
        _candidate = CandidateManager.Instance.CandidatePrefab;
        OnShuffle();
    }

    public void OnShuffle()
    {
        CandidateManager candidateManager = CandidateManager.Instance;

        for (int i = 0; i < _instantiateSpawner.Length; i++)
        {
            if (i < 3)
            {
                CandidateManager.Instance.CreationFile(_instantiateSpawner[i].position, _instantiateSpawner[i].rotation, _candidateParent);
            }
            InputManager.Instance.Shuffle -= OnShuffle;
        }
        CandidateManager.Instance.MinSalary = CandidateManager.Instance.TempSalary;
    }

    private void Update()
    {
        CandidateManager candidateManager = CandidateManager.Instance;

        if (candidateManager.Candidate.Count < 3)
        {
            InputManager.Instance.Shuffle += OnShuffle;
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.Shuffle -= OnShuffle;
    }
    #endregion Methods
}

using UnityEngine;

public class CandidateGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] _instantiateSpawner = null;
    [SerializeField] private Transform _candidateParent = null;

    private void Start()
    {
        InputManager.Instance.Shuffle += OnShuffle;
        for (int i = 0; i < _instantiateSpawner.Length; i++)
        {
            if(i < 3)
            {
                CandidateManager.Instance.CreationFile(_instantiateSpawner[i].position, _instantiateSpawner[i].rotation, _candidateParent);
            }
        }
    }

    public void OnShuffle()
    {
        for (int i = 0; i < _instantiateSpawner.Length; i++)
        {
            if (i < 3)
            {
                CandidateManager.Instance.CreationFile(_instantiateSpawner[i].position, _instantiateSpawner[i].rotation, _candidateParent);
            }
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.Shuffle -= OnShuffle;
    }
}

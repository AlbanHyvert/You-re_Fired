using UnityEngine;

public class MakeAChoice : MonoBehaviour
{
    [SerializeField] private Transform _hiredPos = null;
    [SerializeField] private Transform _firedPos = null;
    [SerializeField] private CandidateGenerator _generator = null;
    public void OnHired()
    {
        InputManager.Instance.Shuffle += _generator.OnShuffle;
        CandidateManager.Instance.OnChoosenFile(_hiredPos.position);
    }

    public void OnFired()
    {
        InputManager.Instance.Shuffle += _generator.OnShuffle;
        CandidateManager.Instance.OnChoosenFile(_firedPos.position);
    }
}

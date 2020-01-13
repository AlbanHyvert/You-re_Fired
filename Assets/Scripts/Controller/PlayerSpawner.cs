using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] Transform _spawnPos = null;
    [SerializeField] Transform _choosenPos = null;

    void Awake()
    {
        PlayerManager.Instance.InstantiatePlayer(_spawnPos.position, _spawnPos.rotation);
        PlayerManager.Instance.Interactions.ChoosenOne = _choosenPos;
    }
}

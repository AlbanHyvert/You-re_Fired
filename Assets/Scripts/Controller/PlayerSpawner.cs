using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] Transform _spawnPos = null;
    [SerializeField] Transform _choosenPos = null;
    #endregion Fields

    void Awake()
    {
        PlayerManager.Instance.InstantiatePlayer(_spawnPos.position, _spawnPos.rotation);
        PlayerManager.Instance.Interactions.ChoosenOne = _choosenPos;
    }
}

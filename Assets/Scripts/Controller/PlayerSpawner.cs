using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] Transform _spawnPos = null;
    [SerializeField] Transform _pickUpPos = null;

    void Start()
    {
        PlayerManager.Instance.InstantiatePlayer(_spawnPos.position, _spawnPos.rotation);
        PlayerManager.Instance.Player.GetComponent<InteractionsController>().PickUpPos = _pickUpPos;
    }
}

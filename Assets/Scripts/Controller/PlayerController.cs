using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Have Interactions ?")]
    [SerializeField] private InteractionsController _interactionsController = null;

    private void Start()
    {
        GameLoopManager.Instance.GetInteractions += _interactionsController.OnUpdate;
    }
}
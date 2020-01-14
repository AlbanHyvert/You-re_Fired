using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    [SerializeField] private Camera _playerCam = null;

    private Transform _choosenOne = null;
    public Transform ChoosenOne { get { return _choosenOne; } set { _choosenOne = value; } }

    [Header("Layers")]
    [SerializeField] private string _pickableTag = "Pickable";
    [SerializeField] private string _interactTag = "Interactible";

    private bool _isPick = false;
    private Rigidbody _objectCache = null;

    public void OnPickUp()
    {
        CandidateFile file = _objectCache.GetComponent<CandidateFile>();

        if(PlayerManager.Instance.Money > file.CandidatePrice)
        {
            _objectCache.position = PlayerManager.Instance.Interactions.ChoosenOne.position;
            _objectCache.useGravity = true;
            _objectCache.isKinematic = false;

            IAction action = _objectCache.GetComponent<IAction>();
            action.Enter();
            InputManager.Instance.Interact -= OnPickUp;
        }
    }

    public void OnUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out hit, 10))
        {
            if(hit.transform.GetComponent<Rigidbody>() != null && hit.transform.GetComponent<CandidateFile>() != null)
            {
                _objectCache = hit.transform.GetComponent<Rigidbody>();

                if (_objectCache.tag.Equals(_pickableTag) && _isPick == false)
                {
                    InputManager.Instance.Interact += OnPickUp;
                }
            }
        }
        else
        {
            if(_objectCache != null)
            {
                _objectCache = null;
            }
            InputManager.Instance.Interact -= OnPickUp;
        }
        Debug.DrawRay(_playerCam.transform.position, _playerCam.transform.forward, Color.black, 10);
    }

    private void OnDestroy()
    {
        InputManager.Instance.Interact -= OnPickUp;
        GameLoopManager.Instance.GetInteractions -= OnUpdate;
    }
}

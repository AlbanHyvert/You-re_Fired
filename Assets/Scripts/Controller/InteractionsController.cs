using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    [SerializeField] private Camera _playerCam = null;

    [SerializeField] private Transform _pickUpPos = null;
    public Transform PickUpPos { get { return _pickUpPos; } set { _pickUpPos = value; } }

    [Header("Layers")]
    [SerializeField] private string _pickableTag = "Pickable";
    [SerializeField] private string _interactTag = "Interactible";

    private bool _isPick = false;
    private Rigidbody _objectCache = null;

    public void OnPickUp()
    {
        _isPick = true;
        _objectCache.transform.SetParent(_pickUpPos);
        _objectCache.transform.position = _pickUpPos.position;
        _objectCache.transform.rotation = _pickUpPos.rotation;

        _objectCache.useGravity = false;
        _objectCache.isKinematic = true;
        _objectCache.detectCollisions = true;

        InputManager.Instance.Interact += OnDrop;
        InputManager.Instance.Interact -= OnPickUp;
    }

    public void OnDrop()
    {
            _objectCache.transform.SetParent(null);
            _objectCache.useGravity = true;
            _objectCache.isKinematic = false;
            _isPick = false;
            _objectCache = null;

        InputManager.Instance.Interact -= OnDrop;
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
                _objectCache.transform.SetParent(null);

                _objectCache.useGravity = true;
                _objectCache.isKinematic = false;
                _isPick = false;
                _objectCache = null;
            }
            InputManager.Instance.Interact -= OnPickUp;
            InputManager.Instance.Interact -= OnDrop;
        }
        Debug.DrawRay(_playerCam.transform.position, _playerCam.transform.forward, Color.black, 10);
    }

    private void OnDestroy()
    {
        InputManager.Instance.Interact -= OnPickUp;
        GameLoopManager.Instance.GetInteractions -= OnUpdate;
    }
}

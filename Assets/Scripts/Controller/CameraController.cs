using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Fields
    [Header("Player")]
    [SerializeField] private Camera _cam = null;

    [Header("Min & Max Y Rotation"), Range(0, -360)]
    [SerializeField] private float _minVertX = -45f;
    [Range(0, 360)]
    [SerializeField] private float _maxVertX = 45f;

    [Header("Min & Max X Rotation"), Range(0, -360)]
    [SerializeField] private float _minVertY = -45f;
    [Range(0, 360)]
    [SerializeField] private float _maxVertY = 45f;

    private float _rotationX = 0f;
    private float _rotationY = 0f;
    #endregion Fields

    #region Methods
    private void Start()
    {
        GameLoopManager.Instance.GetCamera += OnUpdate;
        if (_cam == null)
        {
            _cam = GetComponentInChildren<Camera>();
            if (_cam == null)
            {
                throw new System.Exception("CameraController is trying to access a non-existant object" + _cam + "Exiting");
            }
        }
    }

    private void OnUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _cam.transform.Rotate(0, mouseX * InputFieldManager.Instance.HorizontalSensivity, 0);
        _rotationX -= mouseY * InputFieldManager.Instance.VerticalSensivity;
        _rotationX = Mathf.Clamp(_rotationX, _minVertX, _maxVertX);

        _rotationY += mouseX * InputFieldManager.Instance.HorizontalSensivity;
        _rotationY = Mathf.Clamp(_rotationY, _minVertY, _maxVertY);

        _cam.transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.GetCamera -= OnUpdate;
    }
    #endregion Methods
}

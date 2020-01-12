using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _shuffleTmp = null;

    [SerializeField] private Slider _verticalSensivity = null;
    [SerializeField] private Slider _horizontalSensivity = null;

    [SerializeField] private TextMeshProUGUI _verticalSensivtTmp = null;
    [SerializeField] private TextMeshProUGUI _horizontalSensivityTmp = null;

    private void Start()
    {
        _verticalSensivity.value = InputFieldManager.Instance.VerticalSensivity;
        _horizontalSensivity.value = InputFieldManager.Instance.HorizontalSensivity;

        _shuffleTmp.text = InputFieldManager.Instance.Shuffle.ToString();

        int _tempVertSens = (int)_verticalSensivity.value;
        int _tempHoriSens = (int)_horizontalSensivity.value;

        _horizontalSensivityTmp.text = _tempHoriSens.ToString();
        _verticalSensivtTmp.text = _tempVertSens.ToString();
    }

    public void OnShuffleField()
    {
        InputFieldManager.Instance.Shuffle = _shuffleTmp.text;
    }

    public void OnVerticalSensivity()
    {
        int _tempVertSens = (int)_verticalSensivity.value;

        InputFieldManager.Instance.VerticalSensivity = _tempVertSens;
        _verticalSensivtTmp.text = _tempVertSens.ToString();
    }

    public void OnHorizontalSensivity()
    {
        int _tempHoriSens = (int)_horizontalSensivity.value;

        InputFieldManager.Instance.HorizontalSensivity = _tempHoriSens;
        _horizontalSensivityTmp.text = _tempHoriSens.ToString();
    }
}

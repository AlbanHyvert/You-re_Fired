using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CandidateFile : MonoBehaviour
{
    #region Fields
    #region TextMeshPro
    [Header("TextMeshPro")]
    [SerializeField] private Image _pictureImage = null;
    [SerializeField] private TextMeshProUGUI _firstNameTxt = null;
    [SerializeField] private TextMeshProUGUI _lastNameTxt = null;
    [SerializeField] private TextMeshProUGUI _ageTxt = null;
    [SerializeField] private TextMeshProUGUI _salaryTxt = null;
    [SerializeField] private TextMeshProUGUI _gainTxt = null;
    [SerializeField] private TextMeshProUGUI _rankTxt = null;
    [SerializeField] private TextMeshProUGUI _descriptionTxt = null;
    #endregion TextMeshPro
    #endregion Fields

    #region Properties
    public Image PictureImg { get { return _pictureImage; } set { _pictureImage = value; } }
    public TextMeshProUGUI FirstNameTxt { get { return _firstNameTxt; } set { _firstNameTxt = value; } }
    public TextMeshProUGUI LastNameTxt { get { return _lastNameTxt; } set { _lastNameTxt = value; } }
    public TextMeshProUGUI AgeTxt { get { return _ageTxt; } set { _ageTxt = value; } }
    public TextMeshProUGUI SalaryTxt { get { return _salaryTxt; } set { _salaryTxt = value; } }
    public TextMeshProUGUI GainTxt { get { return _gainTxt; } set { _gainTxt = value; } }
    public TextMeshProUGUI RankTxt { get { return _rankTxt; } set { _rankTxt = value; } }
    public TextMeshProUGUI DescriptionTxt { get { return _descriptionTxt; } set { _descriptionTxt = value; } }
    #endregion Properties
}

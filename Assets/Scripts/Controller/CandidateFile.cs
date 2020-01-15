using Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CandidateFile : MonoBehaviour, IAction
{
    #region Fields
    private int _candidatePrice = 0;
    public int CandidatePrice { get { return _candidatePrice; } set { _candidatePrice = value; } }

    private int _candidateGain = 0;
    public int CandidateGain { get { return _candidateGain; } set { _candidateGain = value; } }

    private int _candidateBounty = 0;
    public int CandidateBounty { get { return _candidateBounty; } }

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
   // public TextMeshProUGUI SalaryTxt { get { return _salaryTxt; } }
   // public TextMeshProUGUI GainTxt { get { return _gainTxt; } }
    public TextMeshProUGUI RankTxt { get { return _rankTxt; } set { _rankTxt = value; } }
    public TextMeshProUGUI DescriptionTxt { get { return _descriptionTxt; } set { _descriptionTxt = value; } }
    #endregion Properties

    public void Start()
    {
        _salaryTxt.text ="Salary: " + UIHelper.FormatIntegerString(_candidatePrice);
        _gainTxt.text = "Gain: " + UIHelper.FormatIntegerString(_candidateGain);
    }

    void IAction.Enter()
    {
        PlayerManager.Instance.Money -= _candidatePrice;
        Scoring.Instance.GlobalGain += _candidateGain;
        CandidateManager.Instance.OnChooseFile(this.gameObject);
    }

    void IAction.Exit()
    {
        Scoring.Instance.GlobalGain -= _candidateGain;
    }
}

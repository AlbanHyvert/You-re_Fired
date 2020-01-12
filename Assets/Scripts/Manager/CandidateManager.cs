using Engine.Singleton;
using System.Collections.Generic;
using UnityEngine;

public class CandidateManager : Singleton<CandidateManager>
{
    #region Fields
    [Header("Candidate File")]
    [SerializeField] private CandidateFile _candidateFile = null;
    [SerializeField] private GameObject _candidateGO = null;

    #region CandidateList
    [Header("Picture")]
    [SerializeField] private Sprite[] _pictureList = null;

    [Header("FirstName")]
    [SerializeField] private string[] _firstNameList = null;

    [Header("LastName")]
    [SerializeField] private string[] _lastNameList = null;

    [Header("Age"), Range(18, 69)]
    [SerializeField] private int _ageMin = 18;

    [Range(18, 69)]
    [SerializeField] private int _ageMax = 69;
    private string _age = null;

    [Header("Rank"), Range(0,5)]
    [SerializeField] private int[] _rankList = null;

    [Header("Salary")]
    [SerializeField] private int _minSalary = 100;
    [SerializeField] private int _maxSalary = 1000000;
    private float _minMultiplicator = 1.1f;
    private float _maxMultiplicator = 2f;
    private int _salaryList = 0;
    private int _gainList = 0;

    [Header("Description"), TextArea(0, 10)]
    [SerializeField] private string[] _descriptionList = null;
    #endregion CandidateList

    private GameObject _candidatePrefab = null;
    public GameObject CandidatePrefab { get { return _candidatePrefab; } }

    List<GameObject> _candidate = null;
    #endregion Fields

    private void Start()
    {
        _candidate = new List<GameObject>();
    }

    private void Shuffle()
    {
        for (int i = 0; i < _pictureList.Length; i++)
        {
            Sprite tempSprite;
            int randomIndex = Random.Range(i, _pictureList.Length);
            tempSprite = _pictureList[randomIndex];
            _pictureList[randomIndex] = _pictureList[i];
            _pictureList[i] = tempSprite;
            _candidateFile.PictureImg.sprite = tempSprite;
        }

        for (int i = 0; i < _firstNameList.Length; i++)
        {
            string tempString;
            int randomIndex = Random.Range(i, _firstNameList.Length);
            tempString = _firstNameList[randomIndex];
            _firstNameList[randomIndex] = _firstNameList[i];
            _firstNameList[i] = tempString;
            _candidateFile.FirstNameTxt.text = tempString.ToString();
        }

        for (int i = 0; i < _lastNameList.Length; i++)
        {
            string tempString;
            int randomIndex = Random.Range(i, _lastNameList.Length);
            tempString = _lastNameList[randomIndex];
            _lastNameList[randomIndex] = _lastNameList[i];
            _lastNameList[i] = tempString;
            _candidateFile.LastNameTxt.text = tempString.ToString();
        }

        int randomAge = Random.Range(_ageMin, _ageMax);
        _age = randomAge.ToString();
        _candidateFile.AgeTxt.text = "Age : " + _age;

        for (int i = 0; i < _descriptionList.Length; i++)
        {
            string tempString;
            int randomIndex = Random.Range(i, _descriptionList.Length);
            tempString = _descriptionList[randomIndex];
            _descriptionList[randomIndex] = _descriptionList[i];
            _descriptionList[i] = tempString;
            _candidateFile.DescriptionTxt.text = "Description : " + tempString.ToString();
        }

        for (int i = 0; i < _rankList.Length; i++)
        {
            int tempInt;
            int randomIndex = Random.Range(i, _rankList.Length);
            tempInt = _rankList[randomIndex];
            _rankList[randomIndex] = _rankList[i];
            _rankList[i] = tempInt;
            _candidateFile.RankTxt.text = "Rank : " + tempInt .ToString();
        }

        if(_salaryList < _maxSalary)
        {
            float randomSalaryMultiplicator = Random.Range(_minMultiplicator, _maxMultiplicator);
            int randomSalary = (int)(_minSalary * randomSalaryMultiplicator);
            _salaryList = randomSalary;
            _candidateFile.SalaryTxt.text = "Salary : " + _salaryList.ToString();
            _minSalary = randomSalary;

            float randomGainMultiplicator = Random.Range(_minMultiplicator, _maxMultiplicator);
            int randomGain = Random.Range(randomSalary, (int)(randomSalary * randomGainMultiplicator));
            _gainList = randomGain;
            _candidateFile.GainTxt.text = "Gain : " + _gainList.ToString();
        }
    }

    public void CreationFile(Vector3 position,Quaternion rotation, Transform parent)
    {
        Shuffle();
        _candidatePrefab =  Instantiate(_candidateGO, position, rotation, parent);
        _candidate.Add(_candidatePrefab);
    }

    public void OnChoosenFile(Vector3 position)
    {
        _candidatePrefab.transform.position = position;
    }

    public void OnRefusenFile(Vector3 position)
    {
        _candidatePrefab.transform.position = position;
    }
}

using Engine.Singleton;
using System.Collections.Generic;
using UnityEngine;

public class CandidateManager : Singleton<CandidateManager>
{
    #region Fields
    [Header("Candidate File")]
    [SerializeField] private CandidateFile _candidateFile = null;
    [SerializeField] private GameObject _candidateGO = null;
    [Header("LEGENDARY Candidate")]
    [SerializeField] private GameObject[] _legendaryGO = null;
    private GameObject _choosenLegendaryGO = null;

    private int _fileNumber = 0;
    private bool _isLegendary = false;
    [SerializeField] private int _legendaryLuck = 95;
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

    private int _salary = 0;
    public int Salary { get { return _salary; } }

    private int _gain = 0;
    public int Gain { get { return _gain; } }

    private int _tempSalary = 0;

    [Header("Description"), TextArea(0, 10)]
    [SerializeField] private string[] _descriptionList = null;
    #endregion CandidateList

    private GameObject _candidatePrefab = null;
    public GameObject CandidatePrefab { get { return _candidatePrefab; } }

    private GameObject _legendaryPrefab = null;
    public GameObject LegendaryPrefab { get { return _legendaryPrefab; } }

    private List<GameObject> _candidate = null;
    public List<GameObject> Candidate { get {return _candidate; } }
    #endregion Fields

    private void Start()
    {
        _candidate = new List<GameObject>();
    }

    private void Shuffle()
    {
        int tempRange = 0;
        float randomSalaryMultiplicator = 0;
        float randomGainMultiplicator = 0;
        int tempLegendary = Random.Range(0, 100);
        
        if(tempLegendary >= _legendaryLuck)
        {
            _isLegendary = true;
            int legendarySize = 0;
            for (int i = 0; i < _legendaryGO.Length; i++)
            {
                legendarySize++;
            }

            _choosenLegendaryGO = _legendaryGO[Random.Range(0, legendarySize)];

            tempRange = Random.Range(3, 5);
        }
        else
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
                int randomIndex = Random.Range(i, _rankList.Length);
                tempRange = _rankList[randomIndex];
                _rankList[randomIndex] = _rankList[i];
                _rankList[i] = tempRange;
            }
        }
        _candidateFile.RankTxt.text = "Rank : " + tempRange.ToString();
        if (_salary < _maxSalary)
        {
            if (tempRange == 1)
            {
                randomSalaryMultiplicator = Random.Range(_minMultiplicator, 1.3f);
            }
            else if (tempRange == 2 || tempRange == 3)
            {
                randomSalaryMultiplicator = Random.Range(1.4f, 1.6f);
            }
            else if (tempRange == 4)
            {
                randomSalaryMultiplicator = Random.Range(1.7f, 1.8f);
            }
            else if (tempRange == 5)
            {
                randomSalaryMultiplicator = Random.Range(1.9f, _maxMultiplicator);
            }

            int randomSalary = (int)(_minSalary * randomSalaryMultiplicator);
            _salary = randomSalary;
            _tempSalary = randomSalary;

            if (tempRange == 1)
            {
                randomGainMultiplicator = Random.Range(_minMultiplicator, 1.3f);
            }
            else if (tempRange == 2 || tempRange == 3)
            {
                randomGainMultiplicator = Random.Range(1.4f, 1.6f);
            }
            else if (tempRange == 4)
            {
                randomGainMultiplicator = Random.Range(1.7f, 1.8f);
            }
            else if (tempRange == 5)
            {
                randomGainMultiplicator = Random.Range(1.9f, _maxMultiplicator);
            }

            int randomGain = Random.Range(randomSalary, (int)(randomSalary * randomGainMultiplicator));
            _gain = randomGain;
        }

    }

    public void CreationFile(Vector3 position,Quaternion rotation, Transform parent)
    {
        _fileNumber++;
        Shuffle();
        _minSalary = _tempSalary;

        if (_isLegendary == false)
        {
            _candidatePrefab = Instantiate(_candidateGO, position, rotation, parent);
            _candidatePrefab.name = "Candidate number: " + _fileNumber;
            CandidateFile file = _candidatePrefab.GetComponent<CandidateFile>();
            file.CandidatePrice = _salary;
            file.CandidateGain = _gain;
            _candidate.Add(_candidatePrefab);
        }

        else
        {
            _legendaryPrefab = Instantiate(_choosenLegendaryGO, position, rotation, parent);
            _legendaryPrefab.name = "Legendary number: " + _fileNumber;
            CandidateFile file = _legendaryPrefab.GetComponent<CandidateFile>();
            file.CandidatePrice = _salary;
            file.CandidateGain = _gain;
            _candidate.Add(_legendaryPrefab);
            _isLegendary = false;
        }
    }

    public void OnClear()
    {
        _minSalary = 100;
        _tempSalary = 0;
        _salary = 0;
        _gain = 0;
        _age = null;

        foreach (GameObject item in _candidate)
        {
            if(item != null)
            {
                Object.Destroy(item);
            }
        }

        if(_fileNumber > 0)
        {
            _fileNumber = 0;
            _candidate.Clear();
        }
    }

    public void OnChooseFile(GameObject savedItem)
    {
        _candidate.Remove(savedItem);
        foreach (GameObject item in _candidate)
        {
            if (item != null)
            {
                Object.Destroy(item);
            }
        }
        _candidate.Clear();
        _fileNumber = 0;
    }
}

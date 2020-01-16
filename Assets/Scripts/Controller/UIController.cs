using UnityEngine;
using TMPro;
using Helper;
public class UIController : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _hud = null;
    [SerializeField] private GameObject _pausedHud = null;
    [SerializeField] private GameObject _settingHud = null;
    [SerializeField] private GameObject _gameOver = null;
    [SerializeField] private TextMeshProUGUI _moneyUi = null;
    [SerializeField] private TextMeshProUGUI _gain = null;
    [SerializeField] private TextMeshProUGUI _timerMinutes_txt = null;
    [SerializeField] private TextMeshProUGUI _timerSeconds_txt = null;
    [SerializeField] private TextMeshProUGUI _scoring_txt = null;

    private int _tempPlayerMoney = 0;
    private int _tempMinutes = 0;
    private int _tempSeconds = 0;
    #endregion Fields

    #region Methods
    private void Start()
    {
        _gameOver.SetActive(false);
        GameLoopManager.Instance.IsPaused = false;
        if (_gain != null)
            _gain.text = Scoring.Instance.GlobalGain.ToString();
        _tempMinutes = Scoring.Instance.TimeMinutes;
        _tempSeconds = Scoring.Instance.TimeSeconds;
        _tempPlayerMoney = PlayerManager.Instance.Money;
        PlayerManager.Instance.UpdateMoney += MoneyUpdate;
        Scoring.Instance.UIGain += GainUpdate;
        Scoring.Instance.MinutesSeconds += TimeUpdate;

        if (_hud != null)
        {
            _hud.SetActive(GameLoopManager.Instance.IsPaused);
            GameLoopManager.Instance.GetCanvas += OnUpdate;
        }
    }

    private void MoneyUpdate(int money)
    {
        if (_moneyUi != null)
        {
            _moneyUi.text = "Money : " + UIHelper.FormatIntegerString(money);
        }
    }

    private void GainUpdate(int gain)
    {
        if (_gain != null)
        {
            _gain.text = "Gain: + " + UIHelper.FormatIntegerString(gain) + "/m";
        }
    }

    private void TimeUpdate(int min, int sec)
    {
        if (_timerMinutes_txt != null && _timerSeconds_txt != null)
        {
            if (sec < 10)
            {
                _timerSeconds_txt.text = "0" + sec.ToString();
            }
            else
            {
                _timerSeconds_txt.text = sec.ToString();
            }

            if (min < 10)
            {
                _timerMinutes_txt.text = "0" + min.ToString() + " : ";
            }
            else
            {
                _timerMinutes_txt.text = min.ToString() + " : ";
            }
        }
    }

    private void OnUpdate()
    {
        if(Scoring.Instance.TimeMinutes <= 0 && Scoring.Instance.TimeSeconds <= 0)
        {
            _gameOver.SetActive(true);
            _scoring_txt.text = UIHelper.FormatIntegerString(PlayerManager.Instance.Money);
        }

        if (GameLoopManager.Instance.IsPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        _hud.SetActive(GameLoopManager.Instance.IsPaused);
    }

    public void OnRestart()
    {
        PlayerManager.Instance.Money = _tempPlayerMoney;
        Scoring.Instance.TimeMinutes = _tempMinutes;
        Scoring.Instance.TimeSeconds = _tempSeconds;
        Scoring.Instance.GlobalGain = 0;
        CandidateManager.Instance.OnClear();
        GameLoopManager.Instance.IsPaused = false;
        _hud.SetActive(GameLoopManager.Instance.IsPaused);
        GameManager.Instance.ChangeState(GameManager.GameStates.GAME);
    }

    public void OnMenu()
    {
        PlayerManager.Instance.Money = _tempPlayerMoney;
        Scoring.Instance.GlobalGain = 0;
        Scoring.Instance.TimeMinutes = _tempMinutes;
        Scoring.Instance.TimeSeconds = _tempSeconds;
        GameLoopManager.Instance.IsPaused = false;
        _hud.SetActive(GameLoopManager.Instance.IsPaused);
        GameManager.Instance.ChangeState(GameManager.GameStates.MAINMENU);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnResume()
    {
        GameLoopManager.Instance.IsPaused = false;
        _pausedHud.SetActive(!GameLoopManager.Instance.IsPaused);
        _settingHud.SetActive(GameLoopManager.Instance.IsPaused);
        _hud.SetActive(GameLoopManager.Instance.IsPaused);
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.GetCanvas -= OnUpdate;
        PlayerManager.Instance.UpdateMoney -= MoneyUpdate;
        Scoring.Instance.UIGain -= MoneyUpdate;
        Scoring.Instance.MinutesSeconds -= TimeUpdate;
        PlayerManager.Instance.UpdateMoney -= MoneyUpdate;
    }
    #endregion Methods
}

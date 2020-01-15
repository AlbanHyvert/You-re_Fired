using UnityEngine;
using TMPro;
using Helper;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _hud = null;
    [SerializeField] private GameObject _pausedHud = null;
    [SerializeField] private GameObject _settingHud = null;
    [SerializeField] private TextMeshProUGUI _moneyUi = null;
    [SerializeField] private TextMeshProUGUI _timerMinutes = null;
    [SerializeField] private TextMeshProUGUI _timerSeconds = null;
    private int _tempPlayerMoney = 0;
    private int _tempMinutes = 0;
    private int _tempSeconds = 0;
    private void Start()
    {
        _tempMinutes = Scoring.Instance.TimeMinutes;
        _tempSeconds = Scoring.Instance.TimeSeconds;
        _tempPlayerMoney = PlayerManager.Instance.Money;
        PlayerManager.Instance.UpdateMoney += MoneyUpdate;
        Scoring.Instance.MinutesSeconds += TimeUpdate;

        if (_hud != null)
        {
            _hud.SetActive(GameLoopManager.Instance.IsPaused);
            Cursor.lockState = CursorLockMode.Confined;
            GameLoopManager.Instance.GetCanvas += OnUpdate;
        }
    }

    private void MoneyUpdate(int value)
    {
        if(_moneyUi != null)
        {
            _moneyUi.text = "Money : " + UIHelper.FormatIntegerString(value);
        }
    }

    private void TimeUpdate(int min, int sec)
    {
        if(_timerMinutes != null && _timerSeconds != null)
        {
            if(sec < 10)
            {
                _timerSeconds.text = "0" + sec.ToString();
            }
            else
            {
                _timerSeconds.text = sec.ToString();
            }

            if(min < 10)
            {
                _timerMinutes.text = "0" + min.ToString() + " : ";
            }
            else
            {
                _timerMinutes.text = min.ToString() + " : ";
            }
        }
    }

    private void OnUpdate()
    {
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

    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.GAME);
    }

    public void OnRestart()
    {
        PlayerManager.Instance.Money = _tempPlayerMoney;
        Scoring.Instance.TimeMinutes = _tempMinutes;
        Scoring.Instance.TimeSeconds = _tempSeconds;
        Scoring.Instance.GlobalGain = 0;
        CandidateManager.Instance.OnClear();
        GameLoopManager.Instance.IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _hud.SetActive(GameLoopManager.Instance.IsPaused);
        GameManager.Instance.ChangeState(GameManager.GameState.GAME);
    }

    public void OnMenu()
    {
        PlayerManager.Instance.Money = _tempPlayerMoney;
        Scoring.Instance.GlobalGain = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameLoopManager.Instance.IsPaused = false;
        _hud.SetActive(GameLoopManager.Instance.IsPaused);
        GameManager.Instance.ChangeState(GameManager.GameState.MAINMENU);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnResume()
    {
        GameLoopManager.Instance.IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _pausedHud.SetActive(!GameLoopManager.Instance.IsPaused);
        _settingHud.SetActive(GameLoopManager.Instance.IsPaused);
        _hud.SetActive(GameLoopManager.Instance.IsPaused);
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.GetCanvas -= OnUpdate;
        PlayerManager.Instance.UpdateMoney -= MoneyUpdate;
    }
}

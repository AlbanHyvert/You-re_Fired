using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _hud = null;
    [SerializeField] private GameObject _pausedHud = null;
    [SerializeField] private GameObject _settingHud = null;
    [SerializeField] private TextMeshProUGUI _moneyUi = null;

    private void Start()
    {
        PlayerManager.Instance.UpdateMoney += MoneyUpdate;

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
            _moneyUi.text = "Money : " + value.ToString();
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
        CandidateManager.Instance.OnClear();
        GameLoopManager.Instance.IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _hud.SetActive(GameLoopManager.Instance.IsPaused);
        GameManager.Instance.ChangeState(GameManager.GameState.GAME);
    }

    public void OnMenu()
    {
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

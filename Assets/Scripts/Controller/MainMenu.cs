using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private int _timeMinutes = 0;
    private int _timeSecond = 0;
    private int _globalTimer = 59;
    private bool _islaunch = false;

    void Start()
    {
        _timeMinutes = Scoring.Instance.TimeMinutes;
        _timeSecond = Scoring.Instance.TimeSeconds;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStart()
    {
         Scoring.Instance.TimeMinutes = _timeMinutes;
         Scoring.Instance.TimeSeconds = _timeSecond;
         Scoring.Instance.GlobalTimer = _globalTimer;

        GameLoopManager.Instance.IsPaused = false;
        GameManager.Instance.ChangeState(GameManager.GameStates.GAME);
    }

    public void OnMenu()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.MAINMENU);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}

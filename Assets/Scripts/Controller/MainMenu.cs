using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStart()
    {
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

    public void OnCredits()
    {
        GameManager.Instance.ChangeState(GameManager.GameStates.CREDITS);
    }
}

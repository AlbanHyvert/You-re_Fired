using UnityEngine.SceneManagement;

public class PreloadState : IGameState
{
    void IGameState.Enter()
    {
        SceneManager.LoadSceneAsync("Preload");
        GameManager.Instance.ChangeState(GameManager.GameStates.MAINMENU);
    }

    void IGameState.Exit()
    {

    }
}

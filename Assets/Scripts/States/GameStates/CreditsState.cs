using UnityEngine.SceneManagement;

public class CreditsState : IGameState
{
    void IGameState.Enter()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    void IGameState.Exit()
    {
        SceneManager.UnloadSceneAsync("Credits");
    }
}

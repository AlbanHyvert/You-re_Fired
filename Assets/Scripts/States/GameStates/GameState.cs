using UnityEngine.SceneManagement;

public class GameState : IGameState
{
    void IGameState.Enter()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    void IGameState.Exit()
    {
        SceneManager.UnloadSceneAsync("Game");
    }
}

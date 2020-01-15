using System.Collections.Generic;
using Engine.Singleton;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    public enum GameStates
    {
        PRELOAD,
        MAINMENU,
        GAME,
        CREDITS
    }

    private GameStates _currentState = GameStates.PRELOAD;
    private Dictionary<GameStates, IGameState> _states = null;
    private Rigidbody _physics = null;
    #endregion Fields

    #region Properties
    public GameStates CurrentState { get { return _currentState; } }
    public IGameState CurrentStateType { get { return _states[_currentState]; } }
    #endregion Properties

    #region Methods
    private void Start()
    {
        _states = new Dictionary<GameStates, IGameState>();
        _states.Add(GameStates.PRELOAD, new PreloadState());
        _states.Add(GameStates.MAINMENU, new MainMenuState());
        _states.Add(GameStates.GAME, new GameState());
        _states.Add(GameStates.CREDITS, new CreditsState());
        _currentState = GameStates.PRELOAD;
        ChangeState(GameStates.MAINMENU);
    }

    public void ChangeState(GameStates nextState)
    {
        _states[_currentState].Exit();
        _states[nextState].Enter();
        _currentState = nextState;
    }
    #endregion Methods
}

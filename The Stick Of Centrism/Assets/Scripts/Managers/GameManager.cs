using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED,
        END
    }

    public EventGameState OnGameStateChanged;

    GameState _currentGameState = GameState.PREGAME;
    
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    private void Update()
    {
        if (_currentGameState == GameState.PREGAME || _currentGameState == GameState.END) 
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void UpdateState(GameState state)
    {
        GameState _previousGameState = _currentGameState;
        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0f;
                break;

            case GameState.END:
                Time.timeScale = 1f;
                break;

            default:
                break;
        }
        OnGameStateChanged.Invoke(_currentGameState, _previousGameState);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        UpdateState(GameState.RUNNING);
    }

    public void GameOver()
    {
        UpdateState(GameState.END);
    }

    public void PauseGame()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(GameState.RUNNING);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

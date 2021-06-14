using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private PauseUI _pauseMenu;
    [SerializeField] private EndUI _endMenu;
    

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(OnStateChangeHandler);
    }

    void OnStateChangeHandler(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (currentState == GameManager.GameState.PAUSED && previousState == GameManager.GameState.RUNNING)
        {
            _pauseMenu.gameObject.SetActive(true);
        }
        if (currentState == GameManager.GameState.RUNNING && previousState == GameManager.GameState.PAUSED)
        {
            _pauseMenu.gameObject.SetActive(false);
        }
        if (currentState == GameManager.GameState.END && previousState == GameManager.GameState.RUNNING)
        {
            _endMenu.gameObject.SetActive(true);
        }
        if (currentState == GameManager.GameState.RUNNING && previousState == GameManager.GameState.END)
        {
            _endMenu.gameObject.SetActive(false);
        }
    }
}

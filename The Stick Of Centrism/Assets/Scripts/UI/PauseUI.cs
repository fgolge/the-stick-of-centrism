using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public Button ResumeButton;
    public Button QuitButton;
    public Button RestartButton;

    private void Awake()
    {
        ResumeButton.onClick.AddListener(ResumeClickHandler);
        QuitButton.onClick.AddListener(QuitClickHandler);
        RestartButton.onClick.AddListener(RestartClickHandler);
    }

    void ResumeClickHandler()
    {
        GameManager.Instance.PauseGame();
    }

    void QuitClickHandler()
    {
        GameManager.Instance.QuitGame();
    }

    void RestartClickHandler()
    {
        GameManager.Instance.RestartGame();
    }
}

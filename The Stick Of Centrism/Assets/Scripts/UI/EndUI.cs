using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public Button RestartButton;
    public Button QuitButton;

    private void Awake()
    {
        RestartButton.onClick.AddListener(RestartClickHandler);
        QuitButton.onClick.AddListener(QuitClickHandler);
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

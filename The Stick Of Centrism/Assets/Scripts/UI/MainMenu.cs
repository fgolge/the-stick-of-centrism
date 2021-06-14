using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;

    private void Awake()
    {
        PlayButton.onClick.AddListener(PlayClickHandler);
        QuitButton.onClick.AddListener(QuitClickHandler);
    }

    public void PlayClickHandler()
    {
        GameManager.Instance.StartGame();
    }

    public void QuitClickHandler()
    {
        GameManager.Instance.QuitGame();
    }
}

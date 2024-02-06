using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionMenu;
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnOptionButton()
    {
        OptionMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnBackButton()
    {
        this.gameObject.SetActive(true);
        OptionMenu.SetActive(false);
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1f;
        AudioManager.Instance.ReplaySound();
        Mushroom.fallSpeed = 2f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

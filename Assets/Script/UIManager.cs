using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI scoreText, livesText, levelText;
    public GameObject gameOverPanel, youWinPanel;
    private void Awake() {
        Instance = this;
    }
    private void Start()
    {
        UpdateScore(0);
        UpdateLives(GameManager.Instance.lives);
        UpdateLevel(GameManager.Instance.currentLevel);   
    }
    public void UpdateScore(int score) => scoreText.text = "Score: " + score;
    public void UpdateLives(int lives) => livesText.text = "Lives: " + lives;
    public void UpdateLevel(int level) => levelText.text = "Level: " + level;
    public void ShowGameOver() => gameOverPanel.SetActive(true);
    public void ShowYouWin() => youWinPanel.SetActive(true);
    public void Replay()
    {
        SceneManager.LoadScene("Gameplay");
        AudioManager.Instance.ReplaySound();
        Time.timeScale = 1f;
        Mushroom.fallSpeed = 2f;
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.TurnOffGameOverSound();
    }
}

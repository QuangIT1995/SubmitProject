using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int lives = 3;
    public int currentLevel = 1; // Màn chơi hiện tại
    public int scoreToNextLevel = 100;
    public float fallSpeedMultiplier = 4f; // Moi level toc do roi tang them
    public SpriteRenderer backgroundRenderer;
    public Sprite[] backgrounds;
    //Cap nhat
    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        if(UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(score);
        }
        if(score >= 350) EndGame();
        CheckLevelUp();
    }
    public void LoseLife()
    {
        lives--;
        if(UIManager.Instance != null)
        {
            UIManager.Instance.UpdateLives(lives);
        }
        if(lives <= 0)
        {
            if(UIManager.Instance != null)
            {
                UIManager.Instance.ShowGameOver();
                AudioManager.Instance.PlayGameOver();
                Time.timeScale = 0;
            }
        }
    }
    private void EndGame()
    {
        if (UIManager.Instance != null) UIManager.Instance.ShowYouWin();
        AudioManager.Instance.PlayGameOver();
        Time.timeScale = 0;
    }
    private void ChangeBackground()
    {
        if(currentLevel - 1 < backgrounds.Length)
        {
            backgroundRenderer.sprite = backgrounds[currentLevel - 1];
        }
    }
    private void CheckLevelUp()
    {
        if(score >= scoreToNextLevel * currentLevel)
        {
            currentLevel++;
            Mushroom.fallSpeed += fallSpeedMultiplier;
            if(UIManager.Instance != null)
            {
                UIManager.Instance.UpdateLevel(currentLevel);
            }
            ChangeBackground();
        }    
    }
}

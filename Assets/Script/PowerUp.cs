using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { ExtraLife, SpeedBoost, DoubleScore}
    public PowerUpType type;
    public float itemFallSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * itemFallSpeed * Time.deltaTime);
        
        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ApplyEffect();
            AudioManager.Instance.PlayCollect();
            Destroy(gameObject);
        }
    }

    private void ApplyEffect()
    {
        if(GameManager.Instance != null)
        {
            switch (type)
            {
                case PowerUpType.ExtraLife:
                    GameManager.Instance.lives++;
                    if(UIManager.Instance != null)
                    {
                        UIManager.Instance.UpdateLives(GameManager.Instance.lives);
                    }
                    break;
                case PowerUpType.SpeedBoost:
                    if(PlayerController.Instance != null)
                    {
                        PlayerController.Instance.BoostSpeed();
                    }
                    break;
                case PowerUpType.DoubleScore:
                    GameManager.Instance.AddScore(20);
                    break;
            }
        }
    }
}

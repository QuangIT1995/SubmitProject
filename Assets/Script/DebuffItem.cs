using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffItem : MonoBehaviour 
{
    public string debuffType = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.ApplyDebuff(debuffType);
            AudioManager.Instance.PlayHit();
            gameObject.SetActive(false);
        }
    }
}
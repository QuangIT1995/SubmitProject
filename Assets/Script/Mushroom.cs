using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public static float fallSpeed = 2f;
    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if(transform.position.y < -5)
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife();
            }
            AudioManager.Instance.PlayHit();
            gameObject.SetActive(false);
        }
    }
}

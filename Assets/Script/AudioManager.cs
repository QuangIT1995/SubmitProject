using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public static AudioManager Instance;
    public AudioSource bgMusic;
    public AudioSource sfxSource;
    public AudioClip jumpSound, dashSound, collectSound, hitSound, gameOverSound;
    private bool isGameOverPlaying = false;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);   
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayJump() => PlaySFX(jumpSound);
    public void PlayDash() => PlaySFX(dashSound);
    public void PlayCollect() => PlaySFX(collectSound);
    public void PlayHit() => PlaySFX(hitSound);
    public void PlayGameOver(){
        bgMusic.Stop();
        PlaySFX(gameOverSound);
        isGameOverPlaying = true;
    } 
    public void ReplaySound(){
        TurnOffGameOverSound();
        bgMusic.Play();
    }
    public void TurnOffGameOverSound()
    {
        if(isGameOverPlaying)
        {
            sfxSource.Stop();
            isGameOverPlaying = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip bgMusic;
    public AudioClip playerWalkSound;
    public AudioClip pistolShoot;
    public AudioClip revolverShoot;
    public AudioClip uziShoot;
    public AudioClip mpvShoot;
    public AudioClip mp8Shoot;
    public AudioClip clickSound;
    public AudioClip skeletonSound;
    public AudioClip zombieSound;
    public AudioClip playerHitSound;


    private void Start()
    {
        musicSource.clip = bgMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        SFXSource.PlayOneShot(sfx);
    }

    public void ClickSFX()
    {
        SFXSource.PlayOneShot(clickSound);
    }
    public void SFXSoruceStop(){
        SFXSource.loop=false;
        SFXSource.Stop();
    }

    public void bgMusicStop()
    {
        musicSource.Stop();
    }
}

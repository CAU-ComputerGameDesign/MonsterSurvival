using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public static SoundEffect instance;
    [SerializeField] private AudioSource bgmPlayer;
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioClip bombSfx;
    [SerializeField] private AudioClip bulletSfx;
    [SerializeField] private AudioClip tentacleSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetVolume(float num)
    {
        bgmPlayer.volume = num;
        sfxPlayer.volume = num;
    }

    public void PlayBomb()
    {
        sfxPlayer.PlayOneShot(bombSfx);
    }

    public void PlayBullet()
    {
        sfxPlayer.PlayOneShot(bulletSfx);
    }

    public void PlayTentacle()
    {
        sfxPlayer.PlayOneShot(tentacleSfx);
    }
}

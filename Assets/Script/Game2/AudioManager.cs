﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            music.volume = PlayerPrefs.GetFloat(PREFS_MUSIC_VOLUME, 0.5f);
        }

    }

    #endregion

    [SerializeField] AudioSource music;
    [SerializeField] AudioSource effects;

    private const string PREFS_MUSIC_VOLUME = "MusicVolume";

    public void PlaySound(AudioClip audio)
    {
        effects.PlayOneShot(audio);
    }

    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
        PlayerPrefs.SetFloat(PREFS_MUSIC_VOLUME, volume);
    }

    public float GetMusicVolume()
    {
        return music.volume;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public bool isMuted;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Main Menu Music");
        isMuted = PlayerPrefs.GetInt("SoundMuted", 0) == 1;
        if (isMuted)
        {
            MusicVolume(0);
            SFXVolume(0);
        }
        else
        {
            MusicVolume(1);
            SFXVolume(1);
        }
    }
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);
        if (sound == null)
        {
            Debug.Log("Can not find music");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(sfxSounds, x => x.name == name);
        if (sfx == null)
        {
            Debug.Log("Can not find music");
        }
        else
        {
            sfxSource.clip = sfx.clip;
            sfxSource.Play();
        }
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    public Sound[] MusicSounds, SFXSounds;
    public AudioSource MusicSource, SFXSource;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start() {
        PlayMusic("Placeholder");
    }

    public void PlayMusic(string name) {
        Sound sound = Array.Find(MusicSounds, x => x.Name == name);
        if (sound == null) Debug.Log("Sound not found");
        else { 
            MusicSource.clip = sound.Clip;
            MusicSource.Play();
        }
    }

    public void PlaySFX(string name) {
        Sound sound = Array.Find(SFXSounds, x => x.Name == name);
        if (sound == null) Debug.Log("Sound not found");
        else SFXSource.PlayOneShot(sound.Clip);
    }

    public void ToggleMusic() { 
        MusicSource.mute = !MusicSource.mute;
    }

    public void ToggleSFX() {
        SFXSource.mute = !SFXSource.mute;
    }

    public void MusicVolume(float volume) { 
        MusicSource.volume = volume;
    }

    public void SFXVolume(float volume) {
        SFXSource.volume = volume;
    }
}
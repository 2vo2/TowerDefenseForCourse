using System;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private SoundLibraryScriptableObject _soundLibrary;
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _buttonClickSound;

    public AudioSource BackgroundMusic => _backgroundMusic;
    public AudioSource ButtonClickSound => _buttonClickSound;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        InitializeBackgroundMusic();
        InitializeButtonClickSound();
    }

    private void InitializeBackgroundMusic()
    {
        _backgroundMusic.clip = _soundLibrary.BackgroundMusic;
        _backgroundMusic.playOnAwake = true;
        _backgroundMusic.loop = true;
        _backgroundMusic.Play();
    }

    private void InitializeButtonClickSound()
    {
        _buttonClickSound.clip = _soundLibrary.ButtonClickSound;
    }

    public void TurnMusic(bool value)
    {
        _backgroundMusic.mute = !value;
    }

    public void ChangeBackgroundMusicVolume(float value)
    {
        _backgroundMusic.volume = value;
    }
}

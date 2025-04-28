using UnityEngine;

public class GameMusic
{
    private AudioSource _musicAudioSource;

    public GameMusic(AudioSource musicAudioSource)
    {
        _musicAudioSource = musicAudioSource;
    }
    
    public void PlayMusic(AudioClip clip)
    {
        _musicAudioSource.clip = clip;
        _musicAudioSource.playOnAwake = true;
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }
}

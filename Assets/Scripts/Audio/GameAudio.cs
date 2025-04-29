using SO;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public static GameAudio Instance;
    
    [SerializeField] private GameAudioScriptableObject _gameAudioData;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _sfxAudioSource;

    public GameAudioScriptableObject GameAudioData => _gameAudioData;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void SaveAudioToggle(int toggle)
    {
        PlayerPrefs.SetInt("AudioToggle", toggle);
    }

    private void SaveAudioVolume(float volume)
    {
        PlayerPrefs.SetFloat("AudioVolume", volume);
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicAudioSource.clip = clip;
        _musicAudioSource.playOnAwake = true;
        _musicAudioSource.loop = true;
        
        _musicAudioSource.mute = !GetAudioToggle();
        _musicAudioSource.volume = GetAudioVolume();
        
        _musicAudioSource.Play();
    }
    
    public void PlaySfx(AudioClip clip)
    {
        _sfxAudioSource.PlayOneShot(clip);
        
        _sfxAudioSource.mute = GetAudioToggle();
        _sfxAudioSource.volume = GetAudioVolume();
    }
    
    public void AudioToggle(bool toggleValue)
    {
        _musicAudioSource.mute = !toggleValue;
        _sfxAudioSource.mute = !toggleValue;
        
        SaveAudioToggle(!toggleValue ? 0 : 1);
    }

    public void AudioVolumeChanged(float value)
    {
        _musicAudioSource.volume = value;
        _sfxAudioSource.volume = value;
        
        SaveAudioVolume(value);
    }

    public bool GetAudioToggle()
    {
        return PlayerPrefs.GetInt("AudioToggle", 1) == 1;
    }

    public float GetAudioVolume()
    {
        return PlayerPrefs.GetFloat("AudioVolume", 1f);
    }
}
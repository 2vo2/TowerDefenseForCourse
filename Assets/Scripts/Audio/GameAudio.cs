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

    public void PlayMusic(AudioClip clip)
    {
        _musicAudioSource.clip = clip;
        _musicAudioSource.playOnAwake = true;
        _musicAudioSource.loop = true;
        
        _musicAudioSource.mute = !GetAudioToggle("MusicToggle");
        _musicAudioSource.volume = GetAudioVolume("MusicVolume");
        
        _musicAudioSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        _sfxAudioSource.clip = clip;
        
        _sfxAudioSource.mute = !GetAudioToggle("SfxToggle");
        _sfxAudioSource.volume = GetAudioVolume("SfxVolume");
        
        _sfxAudioSource.Play();
    }

    public bool GetAudioToggle(string playerPrefsKey)
    {
        return PlayerPrefs.GetInt(playerPrefsKey, 1) == 0;
    }

    public float GetAudioVolume(string playerPrefsKey)
    {
        return PlayerPrefs.GetFloat(playerPrefsKey, 1f);
    }

    public void SetMusicToggle(bool toggleValue)
    {
        _musicAudioSource.mute = !toggleValue;
        
        SaveAudioToggle("MusicToggle", toggleValue ? 0 : 1);
    }

    public void SetSfxToggle(bool toggleValue)
    {
        _sfxAudioSource.mute = !toggleValue;

        SaveAudioToggle("SfxToggle", toggleValue ? 0 : 1);
    }

    private void SaveAudioToggle(string playerPrefsKey, int toggle)
    {
        PlayerPrefs.SetInt(playerPrefsKey, toggle);
    }

    public void SetMusicVolume(float value)
    {
        _musicAudioSource.volume = value;

        SaveAudioVolume("MusicVolume", value);
    }

    public void SetSfxVolume(float value)
    {
        _sfxAudioSource.volume = value;

        SaveAudioVolume("SfxVolume", value);
    }

    private void SaveAudioVolume(string playerPrefsKey, float volume)
    {
        PlayerPrefs.SetFloat(playerPrefsKey, volume);
    }
}
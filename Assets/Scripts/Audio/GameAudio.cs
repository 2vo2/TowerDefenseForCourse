using System;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public static GameAudio Instance;
    
    [SerializeField] private AudioSource _musicAudioSource;
    
    private GameSfx _gameSfx;
    private GameMusic _gameMusic;
    
    public GameSfx GameSfx => _gameSfx;
    public GameMusic GameMusic => _gameMusic;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
        
        _gameSfx = new GameSfx();
        _gameMusic = new GameMusic(_musicAudioSource);
        
        DontDestroyOnLoad(gameObject);
    }
}
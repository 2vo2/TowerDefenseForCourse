using System;
using UnityEngine;

public class TowerSfx : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private AudioSource _audioSource;
    
    private void OnEnable()
    {
        _tower.TowerShoted += OnTowerShoted;
    }

    private void OnDisable()
    {
        _tower.TowerShoted -= OnTowerShoted;
    }

    private void OnTowerShoted(AudioClip clip)
    {
        GameAudio.Instance.GameSfx.PlaySfx(clip);
    }
}
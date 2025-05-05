using System;
using UnityEngine;

public class TowerSfx : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private AudioSource _audioSource;
    
    private TowerPlacer _towerPlacer;
    
    private void OnEnable()
    {
        _towerPlacer = TowerPlacer.Instance;
        
        _tower.TowerShooter.TowerShooted += OnTowerShooted;
        _towerPlacer.OnTowerPlaced += OnTowerPlaced;
    }

    private void OnDisable()
    {
        _tower.TowerShooter.TowerShooted -= OnTowerShooted;
        _towerPlacer.OnTowerPlaced -= OnTowerPlaced;
    }

    private void OnTowerShooted()
    {
        GameAudio.Instance.PlaySfx(_tower.TowerData.ShootSfx);
    }

    private void OnTowerPlaced()
    {
        GameAudio.Instance.PlaySfx(_tower.TowerData.PlaceSfx);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TowerPlacer : MonoBehaviour
{
    public static TowerPlacer Instance;
    
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private MoneySystem _moneySystem;
    [SerializeField] private List<Tower> _towerPrefabs;

    private Tower _activeTower;
    private bool _towerSpawned;
    private bool _isTowerPlaced;

    public bool IsTowerPlaced => _isTowerPlaced;
    
    public event UnityAction OnTowerPlaced;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this) 
            Destroy(Instance);
    }

    private void OnEnable()
    {
        _gameScreen.OnButtonClick += OnButtonClick;
    }

    private void Update()
    {
        PutTowerOnTile();
    }

    private void OnDisable()
    {
        _gameScreen.OnButtonClick -= OnButtonClick;
    }

    private void OnButtonClick(int index)
    {
        if (_moneySystem.MoneyValue >= _towerPrefabs[index].TowerData.Cost)
        {
            _moneySystem.DeductMoney(_towerPrefabs[index].TowerData.Cost);
            
            _activeTower = Instantiate(_towerPrefabs[index], SpawnPosition(), Quaternion.identity);
            _towerSpawned = true;
            _isTowerPlaced = false;
        }
    }

    private void PutTowerOnTile()
    {
        if (!_towerSpawned) return;
        
        _activeTower.transform.position = SpawnPosition();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.TryGetComponent(out TileToPlace tile) && !tile.IsBooked)
            {
                _activeTower.transform.position = tile.transform.position;

                if (Input.GetMouseButtonDown(0))
                {
                    _activeTower = null;
                    _towerSpawned = false;
                    _isTowerPlaced = true;
                    OnTowerPlaced?.Invoke();
                    tile.Booked();
                }
            }
            else
            {
                _activeTower.transform.position = hit.point;
            }
        }
    }

    private Vector3 MousePositionWithCamera()
    {
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
    }

    private Vector3 SpawnPosition()
    {
        return Camera.main.ScreenToWorldPoint(MousePositionWithCamera());
    }
}
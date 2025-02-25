using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private GameInterface _gameInterface;
    [SerializeField] private Tower _towerPrefab;

    private Tower _activeTower;
    private bool _towerSpawned;
    
    private void OnEnable()
    {
        _gameInterface.OnButtonClick += OnButtonClick;
    }

    private void Update()
    {
        PutTowerOnTile();
    }

    private void OnDisable()
    {
        _gameInterface.OnButtonClick -= OnButtonClick;
    }

    private void OnButtonClick()
    {
        _activeTower = Instantiate(_towerPrefab, SpawnPosition(), Quaternion.identity);
        _towerSpawned = true;
    }

    private void PutTowerOnTile()
    {
        if (!_towerSpawned) return;
        
        _activeTower.transform.position = SpawnPosition();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.TryGetComponent(out Tile tile))
            {
                _activeTower.transform.position = tile.transform.position;

                if (Input.GetMouseButtonDown(0))
                {
                    _activeTower = null;
                    _towerSpawned = false;
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
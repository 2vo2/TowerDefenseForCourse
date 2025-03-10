using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance;

    [SerializeField] private Transform _playerBasePoint;
    [SerializeField] private Transform _healthBarParent;
    [SerializeField] private Transform _healthBar;

    private HealthBar _healthBarInstance;
    
    public Transform PlayerBasePoint => _playerBasePoint;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this) 
            Destroy(this);

        _healthBarInstance = new HealthBar();
        
        _healthBarInstance.LookAtCamera(_healthBarParent);
    }
    
    
}

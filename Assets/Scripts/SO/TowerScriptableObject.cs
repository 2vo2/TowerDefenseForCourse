using UnityEngine;

[CreateAssetMenu(menuName = "Tower", fileName = "New Tower Data", order = 0)]
public class TowerScriptableObject : ScriptableObject
{
    [SerializeField] private float _shootRadius;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _shootForce;
    [SerializeField] private int _cost;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private AudioClip _shootSfx;
    [SerializeField] private AudioClip _placeSfx;
    
    public float ShootRadius => _shootRadius;
    public float ShootSpeed => _shootSpeed;
    public float ShootForce => _shootForce;
    public int Cost => _cost;
    public Projectile Projectile => _projectile;
    public AudioClip ShootSfx => _shootSfx;
    public AudioClip PlaceSfx => _placeSfx;
}

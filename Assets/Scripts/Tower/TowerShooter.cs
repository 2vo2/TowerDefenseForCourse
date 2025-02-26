using UnityEngine;

public class TowerShooter
{
    public void Shoot(Projectile projectile, Vector3 shootDirection, float shootForce)
    {
        projectile.Rigidbody.AddForce(shootDirection * shootForce, ForceMode.Acceleration);
    }
}

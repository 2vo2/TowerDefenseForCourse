using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            projectile.gameObject.SetActive(false);
        }
    }
}

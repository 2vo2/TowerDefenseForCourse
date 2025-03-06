using UnityEngine;

public class EnemyUI
{
    public void LookAtCamera(Transform enemyUI)
    {
        enemyUI.LookAt(Camera.main.transform);
    }

    public void ChangeHealthBar(Transform healthBar, float enemyHealth)
    {
        if (healthBar.transform.localScale.x <= 0f) return;
        
        var health = enemyHealth + 1;
        
        var scale = healthBar.localScale;
        scale.x -= healthBar.localScale.x / health;
        healthBar.localScale = scale;
    }
}

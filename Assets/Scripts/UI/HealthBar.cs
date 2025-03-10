using UnityEngine;

public class HealthBar
{
    public void LookAtCamera(Transform heathBarParent)
    {
        heathBarParent.LookAt(Camera.main.transform);
    }

    public void ChangeHealthBar(Transform healthBar, float healthValue)
    {
        if (healthBar.transform.localScale.x <= 0f) return;
        
        var health = healthValue + 1;
        
        var scale = healthBar.localScale;
        scale.x -= healthBar.localScale.x / health;
        healthBar.localScale = scale;
    }
}

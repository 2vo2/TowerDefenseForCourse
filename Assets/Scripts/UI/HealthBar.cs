using UnityEngine;

public class HealthBar
{
    public void LookAtCamera(Transform healthBarParent)
    {
        healthBarParent.LookAt(Camera.main.transform);
    }

    public void ChangeHealthBar(Transform healthBar, float currentHealth, float maxHealth)
    {
        if (maxHealth <= 0f) return;

        var healthPercentage = Mathf.Clamp01(currentHealth / maxHealth);

        var scale = healthBar.localScale;
        scale.x = healthPercentage;
        healthBar.localScale = scale;
    }
}
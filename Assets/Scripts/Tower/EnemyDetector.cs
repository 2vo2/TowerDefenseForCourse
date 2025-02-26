using UnityEngine;

public class EnemyDetector
{
    public Enemy DetectEnemy(Vector3 position, float radius)
    {
        var enemies = Physics.OverlapSphere(position, radius);

        foreach (var enemyInScene in enemies)
        {
            if (enemyInScene.gameObject.TryGetComponent(out Enemy enemy))
            {
                return enemy;
            }
        }

        return null;
    }
    
    public bool IsEnemyInRange(Vector3 towerPosition, Enemy enemy, float radius)
    {
        return Vector3.Distance(towerPosition, enemy.transform.position) <= radius;
    }

}
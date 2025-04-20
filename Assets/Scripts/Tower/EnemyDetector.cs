using UnityEngine;

public class EnemyDetector
{
    public EnemyUnit DetectEnemy(Vector3 position, float radius)
    {
        var enemies = Physics.OverlapSphere(position, radius);

        foreach (var enemyInScene in enemies)
        {
            if (enemyInScene.gameObject.TryGetComponent(out EnemyUnit enemy))
            {
                return !enemy.IsDie ? enemy : null;
            }
        }

        return null;
    }
    
    public bool IsEnemyInRange(Vector3 towerPosition, EnemyUnit enemyUnit, float radius)
    {
        return Vector3.Distance(towerPosition, enemyUnit.transform.position) <= radius;
    }

}
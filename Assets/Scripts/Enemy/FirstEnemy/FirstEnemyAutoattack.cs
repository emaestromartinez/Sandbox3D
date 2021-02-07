using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemyAutoattack : MonoBehaviour
{
    public float autoAttackCooldown;
    public float autoAttackCurrentTime;
    private bool canAttack = true;
    public float autoAttackRange = 100;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }


    List<Transform> GetEnemiesInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, autoAttackRange);
        List<Transform> collidersInRange = new List<Transform>();
        foreach (Collider collider in colliders)
        {
            if (collider && collider.tag == "Player")
            { // if object has the right tag...
                collidersInRange.Add(collider.transform);
            }
        }
        return collidersInRange;
    }
    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    private void BasicAttack(Transform closestEnemy)
    {
        PlayerStats playerStats = closestEnemy.GetComponent<PlayerStats>();
        playerStats.TakeDamage(damage);
    }


    // Update is called once per frame
    void Update()
    {
        autoAttackCurrentTime += Time.deltaTime;
        if (canAttack)
        {
            if (autoAttackCurrentTime < autoAttackCooldown)
            {
                return;
            }
            else
            {
                List<Transform> enemiesInRange = GetEnemiesInRange();
                if (enemiesInRange.Count > 0)
                {
                    Transform closestEnemy = GetClosestEnemy(enemiesInRange);
                    BasicAttack(closestEnemy);
                    autoAttackCurrentTime = 0;
                }
            }
        }
    }
}

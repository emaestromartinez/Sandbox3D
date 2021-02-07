using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    public int xPosition;
    public int yPosition = 125;
    public int zPosition;
    public float generationCooldown = 2f;
    public int maxEnemyCount = 0;
    private int enemyCount = 0;

    private bool isSpawining = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    private void Update()
    {
        if (!isSpawining)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        isSpawining = true;
        while (enemyCount < maxEnemyCount)
        {
            yield return new WaitForSeconds(generationCooldown);
            xPosition = Random.Range(400, 450);
            zPosition = Random.Range(580, 620);
            CalculateProperHeight();
            Instantiate(enemy, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
            enemyCount++;
        }
        isSpawining = false;


    }
    public void EnemyDied()
    {
        if (enemyCount > 0) enemyCount--;
    }
    void CalculateProperHeight()
    {
        if (zPosition <= 595) yPosition = 116;
        else if (zPosition <= 610) yPosition = 120;
        else yPosition = 122;
    }

}

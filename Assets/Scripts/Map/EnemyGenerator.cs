using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField] private int XminPosition;
    [SerializeField] private int XmaxPosition;

    [SerializeField] private int yPosition;

    [SerializeField] private int ZminPosition;
    [SerializeField] private int ZmaxPosition;

    private int xPosition;
    private int zPosition;

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
            Debug.Log("enemy sàwnedd");
            xPosition = Random.Range(XminPosition, XmaxPosition);
            zPosition = Random.Range(ZminPosition, ZmaxPosition);
            Instantiate(enemy, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity);
            enemyCount++;
        }
        isSpawining = false;


    }
    public void EnemyDied()
    {
        if (enemyCount > 0) enemyCount--;
    }


}

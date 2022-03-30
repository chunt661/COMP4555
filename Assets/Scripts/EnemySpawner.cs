using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public int numberToSpawn;
    public float secondsBetweenSpawn;
    public int maxEnemiesOnStage;
    
    private Text counter;
    // Which enemy to spawn
    public GameObject enemyToSpawn;

    private int enemiesRemaining;
    private int currentEnemies;
    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        enemiesRemaining = numberToSpawn;
        timeRemaining = secondsBetweenSpawn;

        counter = GameObject.Find("EnemyCounter").transform.GetChild(0).GetComponent<Text>();
        counter.text = enemiesRemaining.ToString();
    }

    // Spawn an enemy once the timer runs out
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining += secondsBetweenSpawn;

            if (currentEnemies < maxEnemiesOnStage)
            {
                currentEnemies++;
                Instantiate(enemyToSpawn, new Vector3(7, 0, 0), Quaternion.identity);
            }
        }
    }

    void OnEnemyKilled()
    {
        currentEnemies--;
        enemiesRemaining--;
        counter.text = enemiesRemaining.ToString();
    }
}

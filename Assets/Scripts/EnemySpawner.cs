using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public int numberToSpawn;
    public float secondsBetweenSpawn;
    public Text counter;
    public GameObject enemyToSpawn;

    private int enemiesRemaining;
    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        enemiesRemaining = numberToSpawn;
        timeRemaining = secondsBetweenSpawn;

        counter.text = enemiesRemaining.ToString();
    }

    // Spawn an enemy once the timer runs out
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining += secondsBetweenSpawn;
            Instantiate(enemyToSpawn, new Vector3(7, 0, 0), Quaternion.identity);
        }
    }
}

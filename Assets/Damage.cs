using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    // How many times should I be hit before I die
    public int health = 2;
    private EnemySpawner spawner;

    void Start()
    {
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Uncomment this line to check for collision
        //Debug.Log("Hit"+ theCollision.gameObject.name);

        // this line looks for "laser" in the names of 
        // anything collided.
        if (col.name.Contains("Hitbox"))
        {
            health =- 2;

            if (health <= 0)
            {
                Destroy(this.gameObject);
                //spawner.OnEnemyKilled();
                spawner.SendMessage("OnEnemyKilled");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // How many times should I be hit before I die
    public int health = 2;

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
        }
    }
}
}

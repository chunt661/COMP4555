using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Platformer.Mechanics
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class WalkAI : MonoBehaviour

    {
        public int health = 2;
        private EnemySpawner spawner;
        public float speed;
        public float timeToChangeDirection;

        private GameObject player;
        public GameObject effects;
        private float timer;
        private float collisionCooldown = 0f;

        internal AnimationController control;

        // The direction 
        private float direction;

        // Start is called before the first frame update
        void Start()
        {
            spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
            player = GameObject.FindWithTag("Player");
            control = GetComponent<AnimationController>();
            timer = timeToChangeDirection;
            ChangeDirection();
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer += timeToChangeDirection;
                ChangeDirection();
            }

            if (collisionCooldown > 0)
            {
                collisionCooldown -= Time.deltaTime;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null && collisionCooldown <= 0)
            {
                // TODO: hurt player
                control.move.x *= -1;
                timer = timeToChangeDirection;
                collisionCooldown = 1;
            }
            if (collision.gameObject.name.Contains("Player"))
            {

                Destroy(this.gameObject);
                //spawner.OnEnemyKilled();
                spawner.SendMessage("OnEnemyKilled");

            }
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            // Uncomment this line to check for collision
            //Debug.Log("Hit"+ theCollision.gameObject.name);

            // this line looks for "laser" in the names of 
            // anything collided.
            if (col.name.Contains("Hitbox"))
            {
                health = -2;

                if (health <= 0)
                {
                    Destroy(this.gameObject);
                    Instantiate(effects, transform.position, transform.rotation);
                    //spawner.OnEnemyKilled();
                    spawner.SendMessage("OnEnemyKilled");
                }
            }
        }

        void ChangeDirection()
        {
            if (player.transform.position.x > transform.position.x)
            {
                control.move.x = speed;
            }
            else
            {
                control.move.x = -speed;
            }
        }
    }
}
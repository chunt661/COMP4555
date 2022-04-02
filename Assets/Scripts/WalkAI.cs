using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Platformer.Mechanics
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class WalkAI : MonoBehaviour
    {
        public float speed;
        public float timeToChangeDirection;

        private GameObject player;
        private float timer;
        private float collisionCooldown = 0f;

        internal AnimationController control;

        // The direction 
        private float direction;

        // Start is called before the first frame update
        void Start()
        {
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
            var playerHP = collision.gameObject.GetComponent<Health>();

            if (player != null && collisionCooldown <= 0)
            {
                playerHP.Decrement();
                control.move.x *= -1;
                timer = timeToChangeDirection;
                collisionCooldown = 1;
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
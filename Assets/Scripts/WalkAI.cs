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
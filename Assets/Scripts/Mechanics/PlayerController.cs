using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;


namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public GameObject _Object;
        public GameObject _Object2;
        public GameObject _Object3;
        public GameObject _Object4;
        public GameObject _Object5;
        public GameObject _Object6;
        public GameObject _Object7;
        public GameObject _Object8;
        public int playerHP; 

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        public bool squish = false;
      

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            playerHP = 5;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                _Object7.SetActive(true);
                _Object8.SetActive(false);
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                _Object.SetActive(true);
                _Object2.SetActive(false);
                
                _Object4.SetActive(false);
                _Object5.SetActive(false);
                maxSpeed = 7;
            } else if (Input.GetKey(KeyCode.Z)) {
                _Object.SetActive(false);
                _Object2.SetActive(false);
                
                _Object4.SetActive(true);
                _Object5.SetActive(true);
                maxSpeed =0;
            } else {
                _Object.SetActive(false);
                _Object2.SetActive(true);
                
                _Object4.SetActive(false);
                _Object5.SetActive(false);

            }
                        
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                        


                    }
                    break;
                case JumpState.InFlight:
                    
                    
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    _Object6.SetActive(false);
                    _Object3.SetActive(true);
                    squish = true;
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    _Object6.SetActive(true);
                    _Object3.SetActive(false);
                    squish = false;
                    break;
            }
        }
        void OnCollisionEnter2D(Collision2D theCollision)
        {
            // Uncomment this line to check for collision
            //Debug.Log("Hit"+ theCollision.gameObject.name);

            // this line looks for "laser" in the names of 
            // anything collided.
            if (theCollision.gameObject.name.Contains("Enemy"))
            {
                if (!squish)
                {
                    playerHP--;
                }
                if (playerHP < 1)
                {
                    Schedule<PlayerDeath>();
                    _Object7.SetActive(false);
                    _Object8.SetActive(true);
                    controlEnabled = false;
                    playerHP = 5;
                }

            }

            
        }
        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }
            

            if (move.x > 0.01f)
            {
                
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                


            }
            else if (move.x < -0.01f)
            {
               
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                
            }
            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}
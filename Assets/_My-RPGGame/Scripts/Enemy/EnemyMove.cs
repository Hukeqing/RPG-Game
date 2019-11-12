using System;
using Scripts.Player;
using Scripts.Unit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Scripts.Enemy
{
    public class EnemyMove : Health
    {
        public Transform player;
        public float sinkingSpeed = 2;
        
        public bool IsDie { get; private set; }
        
        private bool isStartSinking;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private PlayerControl playerControl;
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        private static readonly int DieHash = Animator.StringToHash("Die");

        private void Start()
        {
            IsDie = false;
            isStartSinking = false;
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            playerControl = player.GetComponent<PlayerControl>();
            curHealth = maxHealth;
        }

        public void Update()
        {
            if (isStartSinking)
            {
                transform.Translate(sinkingSpeed * Time.deltaTime * Vector3.down);
                return;
            }

            if (!playerControl.IsDie)
            {
                animator.SetBool(IsMove, true);
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                animator.SetBool(IsMove, false);
            }
        }

        public void Die()
        {
            if (IsDie) return;
            IsDie = true;
            animator.SetTrigger(DieHash);
        }

        public void StartSinking()
        {
            isStartSinking = true;
            navMeshAgent.enabled = false;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 3);
        }
    }
}
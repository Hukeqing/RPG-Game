using System;
using Scripts.Player;
using Scripts.Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy
{
    public class EnemyMove : Health
    {
        public Transform player;
        
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private PlayerControl playerControl;
        private static readonly int IsMove = Animator.StringToHash("IsMove");

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            playerControl = player.GetComponent<PlayerControl>();
            HealthStart();
        }

        public void Update()
        {
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
    }
}

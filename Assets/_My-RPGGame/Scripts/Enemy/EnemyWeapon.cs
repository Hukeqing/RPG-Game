using System;
using Scripts.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Enemy
{
    public class EnemyWeapon : MonoBehaviour
    {
        public float attackRange;
        public float attackCoolDown;
        public int attackDamage;
        public Transform player;

        private EnemyMove em;
        private float nextAttack;
        private Health playerHealth;

        private void Start()
        {
            nextAttack = Time.time + attackCoolDown;
            playerHealth = player.GetComponent<Health>();
            em = GetComponent<EnemyMove>();
        }

        private void Update()
        {
//            Debug.Log(Vector3.Distance(player.position, transform.position));
            if (!(nextAttack <= Time.time) || !(Vector3.Distance(player.position, transform.position) <= attackRange) ||
                em.IsDie) return;
            nextAttack = Time.time + attackCoolDown;
            playerHealth.ApplyDamage(attackDamage);
//                Debug.Log(1);
        }
    }
}
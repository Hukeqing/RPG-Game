using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Enemy
{
    public class EnemyBoss : MonoBehaviour
    {
        public float bornCoolDown;
        public GameObject[] enemyGameObject;
        public Transform[] enemyBornPoint;

        private float nextBorn;

        private void Start()
        {
            nextBorn = Time.time + bornCoolDown;
        }

        private void Update()
        {
            if (nextBorn > Time.time)
                return;
            foreach (var tar in enemyBornPoint)
            {
                var curEnemy = Random.Range(0, enemyGameObject.Length);
                Instantiate(enemyGameObject[curEnemy], tar);
            }

            nextBorn = Time.time + bornCoolDown;
        }
    }
}

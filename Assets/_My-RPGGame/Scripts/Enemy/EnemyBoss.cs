using System;
using Scripts.Unit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Enemy
{
    public class EnemyBoss : MonoBehaviour
    {
        public float bornCoolDown;
        public Transform[] enemyBornPoint;
        public GameManager gameManager;

        private float nextBorn;

        private void Start()
        {
            nextBorn = Time.time + bornCoolDown;
            gameManager.AddEnemy(gameObject);
        }

        private void Update()
        {
            if (nextBorn > Time.time)
                return;
            foreach (var tar in enemyBornPoint)
            {
                gameManager.TryBornEnemy(tar);
            }
            bornCoolDown = Random.Range(3.0f, 7.0f);
            nextBorn = Time.time + bornCoolDown;
        }
    }
}

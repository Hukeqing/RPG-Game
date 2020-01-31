using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Unit
{
    public class GameManager : MonoBehaviour
    {
        private List<GameObject> enemyList;
        
        public Transform player;
        public GameObject[] enemyGameObject;
        public int enemyMaxNumber;

        private void Start()
        {
            enemyList = new List<GameObject>();
        }

        public void TryBornEnemy(Transform tar)
        {
            UpdateEnemyList();
            if (enemyList.Count >= enemyMaxNumber) return;
            var curEnemy = Random.Range(0, enemyGameObject.Length);
            var newEnemy = Instantiate(enemyGameObject[curEnemy], tar.position, tar.rotation);
            newEnemy.GetComponent<Enemy.EnemyMove>().player = player;
            newEnemy.GetComponent<Enemy.EnemyWeapon>().player = player;
            newEnemy.GetComponent<Enemy.EnemyBoss>().gameManager = this;
        }

        public void AddEnemy(GameObject enemy)
        {
            enemyList.Add(enemy);
        }

        private void UpdateEnemyList()
        {
            for (var i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] != null) continue;
                enemyList.RemoveAt(i);
                i--;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Scripts.Unit
{
    public class GameManager : MonoBehaviour
    {
        private List<GameObject> enemyList;

        public Transform player;
        public GameObject[] enemyGameObject;
        public int enemyMaxNumber;

        public GameObject gameOverCanvas;
        public Text gameOverText;


        private bool isGameOver = false;
        
        private void Start()
        {
            gameOverCanvas.SetActive(false);
            isGameOver = false;
        }

        private void Update()
        {
            if (isGameOver && Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }
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
            if (enemyList == null) enemyList = new List<GameObject>();
            enemyList.Add(enemy);
        }

        private void UpdateEnemyList()
        {
            for (var i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].GetComponent<Enemy.EnemyMove>().IsDie) continue;
                enemyList.RemoveAt(i);
                i--;
            }

            if (enemyList.Count == 0)
            {
                GameOverWin();
            }
        }

        public Transform GetClosestEnemy(Vector3 playerPosition)
        {
            UpdateEnemyList();
            if (enemyList.Count == 0)
                return null;
            var minDistance = Mathf.Infinity;
            Transform minTransform = null;

            foreach (var o in enemyList)
            {
                var curDistance = Vector3.Distance(o.transform.position, playerPosition);
                if (curDistance < minDistance)
                {
                    minTransform = o.transform;
                    minDistance = curDistance;
                }
            }

            return minTransform;
        }

        public void GameOverLoss()
        {
            StartCoroutine(GameOverIEnumerator());
            gameOverText.text = "Game Over";
        }
        
        private void GameOverWin()
        {
            StartCoroutine(GameOverIEnumerator());
            gameOverText.text = "You Win";
        }

        private IEnumerator GameOverIEnumerator()
        {
            gameOverCanvas.SetActive(true);
            yield return new WaitForSeconds(5);
            isGameOver = true;
        }
    }
}
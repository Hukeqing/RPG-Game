using System;
using Scripts.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    public class PlayerControl : Health
    {
        public float moveSpeed;

        public Image playerHealthImage;
        public Image enemyHealthImage;
        public GameManager GM;

        private Vector3 newMove;
        private static readonly int IsMoveHash = Animator.StringToHash("IsMove");

        private void Start()
        {
            animator = GetComponent<Animator>();
            IsDie = false;
            curHealth = maxHealth;
            playerHealth = playerHealthImage;
            enemyHealth = enemyHealthImage;
//            curHealth = maxHealth;
        }

        private void FixedUpdate()
        {
            if (IsDie) return;
//            ApplyDamage(1);
            newMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.Translate(moveSpeed * Time.deltaTime * newMove, Space.World);

            animator.SetBool(IsMoveHash, newMove != Vector3.zero);

            // ReSharper disable once PossibleNullReferenceException
//            var cameraMoveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(cameraMoveRay, out var hit, Mathf.Infinity, 1 << 8 | 1 << 17))
//            {
//                var targetPosition = hit.point;
//                targetPosition.y = transform.position.y;
//                transform.LookAt(targetPosition);
//            }
            transform.LookAt(GM.GetClosestEnemy(transform.position));
        }

        public void Die()
        {
            GM.GameOverLoss();
        }

        public void RestartLevel()
        {
//            Debug.Log(2);
        }
    }
}
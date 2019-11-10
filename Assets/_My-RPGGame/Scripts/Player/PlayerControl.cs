using System;
using Scripts.Unit;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerControl : Health
    {
        public float moveSpeed;

        public bool IsDie { get; private set; }

        private Vector3 newMove;
        private Animator animator;
        private static readonly int IsMoveHash = Animator.StringToHash("IsMove");
        private static readonly int DieHash = Animator.StringToHash("Die");

        private void Start()
        {
            animator = GetComponent<Animator>();
            IsDie = false;
            HealthStart();
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
            var cameraMoveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraMoveRay, out var hit, Mathf.Infinity, 1 << 8))
            {
                var targetPosition = hit.point;
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition);
            }
        }

        public void Die()
        {
            if (IsDie) return;
            IsDie = true;
            animator.SetTrigger(DieHash);
        }

        public void RestartLevel()
        {
//            Debug.Log(2);
        }
    }
}
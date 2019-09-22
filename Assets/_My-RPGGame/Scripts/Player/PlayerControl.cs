using System;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerControl : MonoBehaviour
    {
        public float moveSpeed;

        private Vector3 _newMove;

        private void Update()
        {
            _newMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.Translate(moveSpeed * Time.deltaTime * _newMove, Space.World);

            // ReSharper disable once PossibleNullReferenceException
            var cameraMoveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraMoveRay, out var hit, Mathf.Infinity))
            {
                transform.LookAt(hit.point);
            }
        }
    }
}
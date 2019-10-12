using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Player
{
    public class CameraControl : MonoBehaviour
    {
        public Transform player;
        public float moveValue = 0.05f;

//        public float moveSpeed;
//        public int adjustValue = 20;
//
//        private void Update()
//        {
//            if (Input.mousePosition.x <= adjustValue)
//            {
//                transform.Translate(Time.deltaTime * moveSpeed * -transform.right);
//            }
//            else if (Input.mousePosition.x >= Screen.width - adjustValue)
//            {
//                transform.Translate(Time.deltaTime * moveSpeed * transform.right);
//            }
//
//            if (Input.mousePosition.y <= adjustValue)
//            {
//                transform.Translate(Time.deltaTime * moveSpeed * -transform.forward);
//            }
//            else if (Input.mousePosition.y >= Screen.height - adjustValue)
//            {
//                transform.Translate(Time.deltaTime * moveSpeed * transform.forward);
//            }
//        }
        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, player.position, moveValue);
        }
    }
}
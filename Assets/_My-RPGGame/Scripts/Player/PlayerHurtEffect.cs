using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    public class PlayerHurtEffect : MonoBehaviour
    {
        public Image hurtEffect;
        public float hurtDeep = 0.3f;
        public float hurtCoolDown = 1;

        public void Hurt()
        {
            hurtEffect.color = new Color(1, 0, 0, hurtDeep);
        }

        private void Update()
        {
            if (hurtEffect.color.a > 0)
            {
//                hurtEffect.color = new Color(1, 0, 0, hurtEffect.color.a - hurtDeep / hurtCoolDown * Time.deltaTime);
                hurtEffect.color = new Color(1, 0, 0, Mathf.Lerp(hurtEffect.color.a, 0, hurtCoolDown * Time.deltaTime));
            }
        }
    }
}
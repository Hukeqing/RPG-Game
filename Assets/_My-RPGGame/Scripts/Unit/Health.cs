using Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Unit
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 100;
        public bool IsDie { get; protected set; }
        
        public AudioClip hurt;
        public AudioClip death;
        public AudioSource audioControl;

        public bool isPlayer = false;
        
        protected int curHealth;
        protected Animator animator;

        private static readonly int DieHash = Animator.StringToHash("Die");

        protected static Image playerHealth;
        protected static Image enemyHealth;

        public void ApplyDamage(int damage)
        {
            if (IsDie)
                return;
            curHealth -= damage;
            if (curHealth <= 0)
            {
                IsDie = true;
                curHealth = 0;
                animator.SetTrigger(DieHash);
                SendMessage("Die");
                audioControl.clip = death;
            }
            else
            {
                audioControl.clip = hurt;
            }
            audioControl.Stop();
            audioControl.Play();
            if (isPlayer)
            {
                playerHealth.fillAmount = (float)curHealth / maxHealth;
            }
            else
            {
                enemyHealth.fillAmount = (float) curHealth / maxHealth;
            }
        }       
        public void Die()
        {
        }

    }
}
using Scripts.Player;
using UnityEngine;

namespace Scripts.Unit
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 100;

        private int curHealth;

        private void Start()
        {
            curHealth = maxHealth;
        }

        public void ApplyDamage(int damage)
        {
            curHealth -= damage;
            if (curHealth < 0)
            {
                GetComponent<PlayerControl>().Die();
//                Destroy(gameObject);
            }
        }
    }
}

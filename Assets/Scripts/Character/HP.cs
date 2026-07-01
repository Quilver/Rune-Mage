using System;
using UnityEngine;
namespace Character
{
    public class HP : MonoBehaviour
    {
        public int hp = 10;
        public Action OnStun;
        public Action<int> OnHpChanged;
        public void TakeDamage(int damage)
        {
            hp -= damage;
            OnHpChanged?.Invoke(hp);
            if (hp <= 0)
            {
                Die();
            }
        }
        public void SetHP(int hp)
        {
            this.hp = hp;
            OnHpChanged?.Invoke(hp);
        }
        public void Heal(int healAmount)
        {
            hp += healAmount;
            OnHpChanged?.Invoke(hp);
            // Optionally, you can add a maximum HP limit here
        }
        [SerializeField] bool player = false;
        private void Die()
        {
            if (player)
            {
                transform.position = new Vector3(1, -7.5f, 0);
                GetComponent<PlayerControls>().Reset();
                CameraTransition.instance.Reset();
                return;
            }
            // Handle death logic here
            Destroy(gameObject);
        }
    }

}

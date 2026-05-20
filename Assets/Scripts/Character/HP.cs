using System;
using UnityEngine;

public class HP : MonoBehaviour
{
    public int hp = 10;
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
    public void Heal(int healAmount)
    {
        hp += healAmount;
        OnHpChanged?.Invoke(hp);
        // Optionally, you can add a maximum HP limit here
    }
    private void Die()
    {
        // Handle death logic here
        Debug.Log("Character has died.");
        Destroy(gameObject);
    }
}

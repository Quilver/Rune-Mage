using UnityEngine;
using UnityEngine.UI;

public class EnemyHP_UI : MonoBehaviour
{
    HP hp;
    float maxHP;
    Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = GetComponentInParent<HP>();
        hp.OnHpChanged += UpdateHP;
        maxHP = hp.hp;
        slider = GetComponent<Slider>();
    }

    void UpdateHP(int hp)
    {
        slider.value = hp / maxHP;
    }
}

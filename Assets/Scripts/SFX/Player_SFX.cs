using UnityEngine;
namespace SFX
{
    public class Player_SFX : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Character.HP>().OnHpChanged += hpChange;
            GetComponent<PlayerControls>().onSpellCast += Attack;
        }
        [SerializeField] Audio damage, attack, death;
        void Attack()
        {
            //SFX_Manager.instance.PlaySFXClip(attack.clip, transform, attack.volume);
        }
        void hpChange(int hp)
        {
            if (hp > 0)
                SFX_Manager.instance.PlaySFXClip(damage.clip, transform, damage.volume);
            else
                SFX_Manager.instance.PlaySFXClip(death.clip, transform, death.volume);
        }
    }

}

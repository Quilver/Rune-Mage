using UnityEngine;
namespace SFX
{
    public class NPC_SFX : MonoBehaviour
    {

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GetComponent<Character.HP>().OnHpChanged += hpChange;
            GetComponent<Character.HP>().OnStun += Stun;
            GetComponent<NPC.EnemyAI>().Spot += Spot;
            GetComponent<NPC.EnemyAI>().OnAttack += Attack;
        }
        [SerializeField] Audio damage, death, attack, stun, spot;
        void Attack()
        {
            SFX_Manager.instance.PlaySFXClip(attack.clip, transform, attack.volume);
        }
        void Spot()
        {
            Debug.Log("Spotted");
            SFX_Manager.instance.PlaySFXClip(spot.clip, transform, spot.volume);
        }
        void Stun()
        {
            SFX_Manager.instance.PlaySFXClip(stun.clip, transform, stun.volume);
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

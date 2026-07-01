using UnityEngine;
namespace Traps
{
    public class Pit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.attachedRigidbody.GetComponentInParent<Character.HP>() != null)
            {
                Debug.Log(collision.gameObject.name + " has fallen into the pit!");
                collision.attachedRigidbody.GetComponentInParent<Character.HP>().TakeDamage(9999);
            }
        }
    }

}

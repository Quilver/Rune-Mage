using UnityEngine;

public class OnContact : MonoBehaviour
{
    [SerializeField]
    Reflect reflect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Reflecting:"+collision.gameObject);
        reflect.ApplyEffect(collision.gameObject, gameObject);
    }
}

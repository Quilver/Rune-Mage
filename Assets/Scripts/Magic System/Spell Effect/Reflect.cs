using UnityEngine;
[CreateAssetMenu(fileName = "Reflect", menuName = "Scriptable Objects/Spell Effect/Reflect")]
public class Reflect : ISpellEffect
{
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        Debug.Log("Reflect");
        Vector2 toPlayer = caster.transform.position - target.transform.position;
        Rigidbody2D rigidbody2D = target.GetComponent<Rigidbody2D>();
        float dot = Vector2.Dot(toPlayer, rigidbody2D.linearVelocity);
        float speed = Mathf.Max(0, dot);
        rigidbody2D.linearVelocity -= 2*toPlayer.normalized * speed;
    }
}

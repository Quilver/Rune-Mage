using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    SpriteRenderer[] sprites;
    Animator animator;
    [SerializeField] Animator staff;
    [SerializeField] Transform light, wand;
    [SerializeField]
    PlayerMovement movement;
    void Start()
    {
        
        CastingInput casting;
        PlayerControls controls = GetComponentInParent<PlayerControls>();
        controls.onRuneUsed += Casting;
        animator = GetComponent<Animator>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }
    void Casting(Rune rune, int uses)
    {
        staff.SetBool("Cast", true);
        StartCoroutine(_casting());
    }
    IEnumerator _casting() { yield return null; staff.SetBool("Cast", false); }
    // Update is called once per frame
    void Update()
    {
        var velocity = movement.moveDirection;
        if (velocity.magnitude > 0.01f)
        {
            
            animator.SetBool("Moving", true);
            animator.speed = velocity.magnitude / 4;
        }
        else
            animator.SetBool("Moving", false);
        bool movingRight = wand.localPosition.x > 0;
        Moving(movingRight);
    }
    void Moving(bool right)
    {
        foreach (var item in sprites)
            item.flipX = right;
        float x = Mathf.Abs(light.localPosition.x);
        if (right)
            light.localPosition = new Vector3(x, light.localPosition.y, 0);
        else
            light.localPosition = new Vector3(-x, light.localPosition.y, 0);
    }
}

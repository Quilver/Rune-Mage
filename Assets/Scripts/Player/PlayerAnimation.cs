using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    SpriteRenderer[] sprites;
    Animator animator;
    [SerializeField] Animator staff;
    [SerializeField] Transform staffLight;
    [SerializeField] Transform wand;
    [SerializeField]
    PlayerMovement movement;
    void Start()
    {
        
        PlayerControls controls = GetComponentInParent<PlayerControls>();
        controls.onRuneUsed += Casting;
        animator = GetComponent<Animator>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }
    void Casting(Rune rune, int uses)
    {
        staff.SetBool("Cast", true);
        StartCoroutine(casting());
    }
    IEnumerator casting() { yield return null; staff.SetBool("Cast", false); }
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
        float x = Mathf.Abs(staffLight.localPosition.x);
        if (right)
            staffLight.localPosition = new Vector3(x, staffLight.localPosition.y, 0);
        else
            staffLight.localPosition = new Vector3(-x, staffLight.localPosition.y, 0);
    }
}

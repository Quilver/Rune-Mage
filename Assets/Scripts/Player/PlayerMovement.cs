using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    InputActionReference moveControl;
    [SerializeField] float moveSpeed = 5f;
    public Vector2 moveDirection;
    void OnEnable()
    {
        moveControl.action.Enable();
        rb= GetComponent<Rigidbody2D>();
    }
    void OnDisable()
    {
        moveControl.action.Disable();
    }
    void FixedUpdate()
    {
        moveDirection = moveControl.action.ReadValue<Vector2>();
        Move();
    }
    void Move()
    {
        GetComponent<Rigidbody2D>().linearVelocity = moveDirection * moveSpeed;
    }

}

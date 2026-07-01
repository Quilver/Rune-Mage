using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
namespace MagicSystem
{
    public class Wand : MonoBehaviour
    {
        [SerializeField]
        InputActionReference lookControl, mouseLook;
        [SerializeField, Range(0.3f, 2)] float radius;
        public Vector2 lookDirection;
        [SerializeField] SpellSystem.SpellData spell;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        private void OnEnable()
        {
            lookControl.action.Enable();
            mouseLook.action.Enable();
        }
        private void OnDisable()
        {
            lookControl.action.Disable();
            mouseLook.action.Disable();
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (lookControl.action.WasPerformedThisFrame())
            {
                lookDirection = lookControl.action.ReadValue<Vector2>();
                Look();
            }
            if(mouseLook.action.WasPerformedThisFrame())
            {
                Vector2 mousePos = mouseLook.action.ReadValue<Vector2>();
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
                lookDirection = worldMousePos - transform.position;
                Look();
            }
        }
        void Look()
        {
            transform.localPosition = lookDirection.normalized * radius;
            transform.up = lookDirection;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, 0.2f);
            Gizmos.DrawRay(transform.position, transform.up * 2f);
        }
    }
}
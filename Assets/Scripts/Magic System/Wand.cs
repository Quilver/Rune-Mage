using MagicSystem.Spell;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
namespace MagicSystem
{
    public class Wand : MonoBehaviour
    {
        [SerializeField]
        InputActionReference lookControl;
        [SerializeField, Range(0.3f, 2)] float radius;
        public Vector2 lookDirection;
        [SerializeField] ISpellTarget spell;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        private void OnEnable()
        {
            lookControl.action.Enable();
        }
        private void OnDisable()
        {
            lookControl.action.Disable();
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (lookControl.action.WasPerformedThisFrame())
            {
                lookDirection = lookControl.action.ReadValue<Vector2>();
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
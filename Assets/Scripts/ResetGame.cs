using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField]
    InputActionReference pause;
    private void OnEnable()
    {
        pause.action.performed += Restart;
    }
    private void OnDisable()
    {
        pause.action.performed -= Restart;
    }
    void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(0);
    }
}

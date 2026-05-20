using UnityEngine;
using UnityEngine.InputSystem;
public class CastingInput : MonoBehaviour
{
    PlayerControls _playerControls;
    PlayerControls PlayerControls
    {
        get
        {
            if (_playerControls == null)
                _playerControls = GetComponentInParent<PlayerControls>();
            return _playerControls;
        }
    }
    [SerializeField]
    InputActionReference wind, fire, earth, lightning, projectionCast, internalCast;
    void OnEnable()
    {
        wind.action.performed += CastWind;
        fire.action.performed += CastFire;
        earth.action.performed += CastEarth;
        lightning.action.performed += CastLightning;
        projectionCast.action.performed += ProjectionCast;
        internalCast.action.performed += InternalCast;
    }
    void OnDisable()
    {
        wind.action.performed -= CastWind;
        fire.action.performed -= CastFire;
        earth.action.performed -= CastEarth;
        lightning.action.performed -= CastLightning;
        projectionCast.action.performed -= ProjectionCast;
        internalCast.action.performed -= InternalCast;
    }


    void CastRune(Rune rune) { 
        Debug.Log($"Cast {rune}");
        PlayerControls.AddRune(rune);
        
    }
    void ProjectionCast(InputAction.CallbackContext context)
    {
        Debug.Log("Projection Cast");
        PlayerControls.CastSpell(true);
    }
    void InternalCast(InputAction.CallbackContext context)
    {
        Debug.Log("Internal Cast");
        PlayerControls.CastSpell(false);
    }

    void CastWind(InputAction.CallbackContext context)
    {
        if(PlayerControls.wind.UsesLeft > 0)
            CastRune(PlayerControls.wind);
    }
    void CastFire(InputAction.CallbackContext context)
    {
        if(PlayerControls.fire.UsesLeft > 0)
            CastRune(PlayerControls.fire);
    }
    void CastEarth(InputAction.CallbackContext context)
    {
        if(PlayerControls.earth.UsesLeft > 0)
            CastRune(PlayerControls.earth);
    }
    void CastLightning(InputAction.CallbackContext context)
    {
        if(PlayerControls.lightning.UsesLeft > 0)
            CastRune(PlayerControls.lightning);
    }
}

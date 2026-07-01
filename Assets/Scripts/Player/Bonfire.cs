using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bonfire : MonoBehaviour
{
    [SerializeField]
    SFX.Audio restJingle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerControls>() != null)
        {
            PlayerControls player = collision.GetComponent<PlayerControls>();
            Rest(player);
        }
    }
    IEnumerator RestCoroutine()
    {
        GetComponent<Light2D>().intensity = 4;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            GetComponent<Light2D>().intensity = Mathf.Lerp(4, 1.5f, t);
            yield return null;
        }
    }
    public void Rest(PlayerControls player)
    {
        player.Reset();
        StartCoroutine(RestCoroutine());
        SFX.SFX_Manager.instance.PlaySFXClip(restJingle.clip, transform, restJingle.volume);
    }
}

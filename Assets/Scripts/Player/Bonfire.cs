using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bonfire : MonoBehaviour
{

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
            GetComponent<Light2D>().intensity = Mathf.Lerp(3, 2.5f, t);
            yield return null;
        }
    }
    public void Rest(PlayerControls player)
    {
        StartCoroutine(RestCoroutine());
    }
}

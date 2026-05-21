using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
public class Reward : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControls>() != null)
        {
            PlayerControls player = collision.GetComponent<PlayerControls>();
            RewardEffect(player);
        }
    }
    public void RewardEffect(PlayerControls player)
    {
        Destroy(gameObject);
    }
}

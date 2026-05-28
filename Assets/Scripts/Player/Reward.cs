using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
public class Reward : MonoBehaviour
{
    [SerializeField]
    Audio rewardJingle;
    public enum RewardType
    {
        Fire, Earth, Lightning, Level
    }
    public RewardType type;
    [SerializeField]
    GameObject door;
    public PlayerData levelup;
    void Start()
    {
        
    }
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
        SFX_Manager.instance.PlaySFXClip(rewardJingle.clip, transform, rewardJingle.volume);

        switch (type)
        {
            case RewardType.Fire:
                Debug.Log("unlocked fire");
                player.UnlockRune(player.fire);
                break;
            case RewardType.Earth:
                player.UnlockRune(player.earth);
                break;
            case RewardType.Lightning:
                player.UnlockRune(player.lightning);
                break;
            case RewardType.Level:
                player.LevelUp(levelup);
                door.SetActive(false);
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}

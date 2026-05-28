using UnityEngine;
[System.Serializable]
struct Audio
{
    public AudioClip clip;
    [Range(0f, 1f)] 
    public float volume;
}
public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager instance;
    [SerializeField]
    GameObject soundFXObject;
    private void Awake()
    {
        instance = this;
    }
    public void PlaySFXClip(AudioClip clip, Transform spawn, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawn.position, Quaternion.identity).GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        float clipLength = audioSource.clip.length;

        audioSource.Play();
        Destroy(audioSource, clipLength);

    }
}

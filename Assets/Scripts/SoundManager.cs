using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip normalCloudHit;
    private void Start()
    {
        PlayerMovement.Instance.OnNormalCloudHit += Player_OnNormalCloudHit;
    }

    private void Player_OnNormalCloudHit(object sender, System.EventArgs e)
    {
        audioSource.PlayOneShot(normalCloudHit);
    }
}

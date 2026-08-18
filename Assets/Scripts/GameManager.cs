using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    int speed = 200;
    private void Start()
    {
        PlayerMovement.Instance.OnNormalCloudHit += Player_OnNormalCloudHit;
    }

    private void Update()
    {
        speedText.text = speed.ToString() + "m/s";
    }
    private void Player_OnNormalCloudHit(object sender, System.EventArgs e)
    {
        speed -= 20;
    }
}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] Canvas mainMenu;
    [SerializeField] Button start;
    [SerializeField] LevelManager levelManager;
    [SerializeField] CloudSpawner cloudSpawner;
    float speed = 200;
    int currentLevel = 0;
    private void Start()
    {
        PlayerMovement.Instance.OnNormalCloudHit += Player_OnNormalCloudHit;
        if (currentLevel == 0)
        {
            mainMenu.gameObject.SetActive(true);
            start.onClick.AddListener(() =>LoadLevel(1));
        }
    }

    private void Update()
    {
        speed += Time.deltaTime * 9;
        speedText.text = $"{speed:0}" + "m/s";
    }
    private void Player_OnNormalCloudHit(object sender, System.EventArgs e)
    {
        speed -= 20;
    }

    void LoadLevel(int level)
    {
        cloudSpawner.SpawnClouds(levelManager.GetLevelDifficulty(level));
    }
}

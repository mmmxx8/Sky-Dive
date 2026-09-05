using System.Collections.Generic;
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

    public void LoadMainMenu()
    {
        PauseGame();
        mainMenu.gameObject.SetActive(true);
    }
    private void LoadLevel(int level)
    {
        cloudSpawner.SpawnClouds(levelManager.GetLevelDifficulty(level));
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void ResetData()
    {
        speed = 200;
        GameObject[] clouds = FindClouds();
        foreach(GameObject cloud in clouds)
        {
            Destroy(cloud);
        }
    }
    private GameObject[] FindClouds()
    {
        GameObject[] clouds; //TODO: make it a list
        GameObject[] normalClouds = GameObject.FindGameObjectsWithTag("NormalCloud");
        //find dense
        //find chemical
        //find storm
        return normalClouds;
    }
}

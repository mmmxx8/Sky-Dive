using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] Canvas mainMenu;
    [SerializeField] Button start;
    [SerializeField] Button startAfterEnd;
    [SerializeField] LevelManager levelManager;

    float speed = 200;
    int currentLevel = 0;
    float multiplier =9.8f;
    bool alreadySpedUp=false;
    float playerHealth = 200;
    private void Awake()
    {
        PlayerPrefs.SetInt("currentLevel", 1);
    }
    private void Start()
    {
        CloudSpawner.Instance.OnSpeedUp += Spawner_OnSpeedUp;
        CloudSpawner.Instance.OnDurationOver += Spawner_OnDurationOver;
        PlayerMovement.Instance.OnNormalCloudHit += Player_OnNormalCloudHit;
        if (currentLevel == 0)
        {
            mainMenu.gameObject.SetActive(true);
            start.onClick.AddListener(() =>LoadLevel(PlayerPrefs.GetInt("currentLevel")));
        }
        
    }

    private void Spawner_OnDurationOver(object sender, EventArgs e)
    {
        startAfterEnd.gameObject.SetActive(true);
        playerHealth -= 0.5f * speed;
        Time.timeScale = 0f;
        if (playerHealth <=100)
        {
            ResetData();
            startAfterEnd.onClick.AddListener(() => LoadLevel(PlayerPrefs.GetInt("currentLevel")));
            Debug.Log("WASTED");
        }
        else
        {
            ResetData();
            Debug.Log("Congrats!");
            startAfterEnd.onClick.AddListener(() => LoadLevel(PlayerPrefs.GetInt("currentLevel") + 1));
        }
           
    }

    private void Spawner_OnSpeedUp(object sender, EventArgs e)
    {
        if (!alreadySpedUp)
        {
            multiplier *= 1.1f;
            alreadySpedUp = true;
            
        }
        else
        {
            multiplier += 1.2f;
        }
    }

    private void Update()
    {
        speed += Time.deltaTime * multiplier;
        speedText.text = $"{speed:0}" + "m/s";
    }
    private async void Player_OnNormalCloudHit(object sender, System.EventArgs e)
    {
        speed -= 20;
        if (speed <= 0)
        {
            speed = 0;
        }
        await ChangeTextColor(Color.green);

    }

    private async Task ChangeTextColor(Color color)
    {
        speedText.color = color;
        await Task.Delay(TimeSpan.FromMilliseconds(100));
        speedText.color = Color.white;
    }

    public void LoadMainMenu()
    {
        PauseGame();
        mainMenu.gameObject.SetActive(true);
    }
    private void LoadLevel(int level)
    {
        startAfterEnd.gameObject.SetActive(false);
        Time.timeScale = 1f;
        LevelManager.LevelDifficulty levelDifficulty = levelManager.GetLevelDifficulty(level);
        CloudSpawner.Instance.SpawnClouds(levelDifficulty);
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
        CloudSpawner.Instance.ResetData();
        speed = 200;
        playerHealth = 200;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> normalClouds;
    [SerializeField] private List<GameObject> denseClouds;
    [SerializeField] private List<GameObject> chemicalClouds;
    [SerializeField] private List<GameObject> stormClouds;
    public static CloudSpawner Instance { get; private set; }
    float screenLeftEdge;
    float screenRightEdge;
    float timePassed = 0;
    float currentSpawnSpeed;
    float speedUp;
    bool firstSpeedUp = true;
    bool secondSpeedUp = true;
    public event EventHandler OnSpeedUp;
    public event EventHandler OnDurationOver;
    private void Awake()
    {
        screenLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        Instance = this;
    }
    private void Update()
    {
        timePassed += Time.deltaTime;
    }
    public void SpawnClouds(LevelManager.LevelDifficulty difficulty)
    {
        currentSpawnSpeed = difficulty.startingCloudSpeed;
        speedUp = difficulty.levelDuration / 3;
        StartCoroutine(WaitAndSpawn(difficulty));
    }

    private IEnumerator WaitAndSpawn(LevelManager.LevelDifficulty difficulty)
    {

        while (true)
        {
            float spawnTime = Random.Range(difficulty.minSpawnInterval, difficulty.maxSpawnInterval);
            yield return new WaitForSeconds(spawnTime);
            SpawnCloud(difficulty);

            if ((timePassed - speedUp >= 0.5f) && firstSpeedUp)
            {
                OnSpeedUp?.Invoke(this, EventArgs.Empty);
                firstSpeedUp = false;
                currentSpawnSpeed *= 1.1f;
            }
            if ((timePassed - (speedUp * 2) >= 0.5f) && secondSpeedUp)
            {
                OnSpeedUp?.Invoke(this, EventArgs.Empty);
                secondSpeedUp = false;
                currentSpawnSpeed *= 1.2f;
            }
            if (timePassed > difficulty.levelDuration)
            {
                OnDurationOver?.Invoke(this, EventArgs.Empty);
                break;
            }
        }
    }

    private void SpawnCloud(LevelManager.LevelDifficulty difficulty)
    {
        float randomValue = Random.value;

        //treating cloud probablity as a range from 0 to 1
        float normalRange = difficulty.normalCloudProbability;
        float denseRange = difficulty.denseCloudProbability + normalRange;
        float chemicalRange = difficulty.chemicalCloudProbability + denseRange;

        if (randomValue <= normalRange)
        {
            Spawn(normalClouds);
        }
        else if (randomValue <= denseRange)
        {
            Spawn(denseClouds);
        }
        else if (randomValue <= chemicalRange)
        {
            Spawn(chemicalClouds);
        }
        else
            Spawn(stormClouds);
    }

    private void Spawn(List<GameObject> clouds)
    {
        if (clouds.Count > 0)
        {
            float xSpawn = Random.Range(screenLeftEdge, screenRightEdge);
            Vector3 spawnPosition = new(xSpawn, -6, 0);
            GameObject cloud = clouds[Random.Range(0, clouds.Count)];
            cloud = Instantiate(cloud, spawnPosition, Quaternion.identity);
            cloud.GetComponent<ObstacleMovement>().Init(currentSpawnSpeed);
        }

    }
    public void ResetData()
    {
        timePassed = 0;
        firstSpeedUp = true;
        secondSpeedUp = true;
    }
}

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
    float screenLeftEdge;
    float screenRightEdge;
    float timePasased=0;
    private void Awake()
    {
        screenLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }
    private void Update()
    {
        timePasased += Time.deltaTime;
        Debug.Log(timePasased);
    }
    public void SpawnClouds(LevelManager.LevelDifficulty difficulty)
    {

        StartCoroutine(WaitAndSpawn(difficulty));
    }

    private IEnumerator WaitAndSpawn(LevelManager.LevelDifficulty difficulty)
    {

        while (true)
        {
            float spawnTime = Random.Range(difficulty.minSpawnInterval, difficulty.maxSpawnInterval);
            yield return new WaitForSeconds(spawnTime);
            SpawnCloud(difficulty);

            if (timePasased > difficulty.levelDuration)
            {
                //level done
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
            Vector3 spawnPosition = new Vector3(xSpawn, -6, 0);
            GameObject cloud = clouds[Random.Range(0, clouds.Count)];
            Instantiate(cloud, spawnPosition, Quaternion.identity);
        }

    }
}

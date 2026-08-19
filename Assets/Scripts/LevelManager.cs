using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelDifficulty> levelDifficulties = new();

    [System.Serializable]
    public class LevelDifficulty
    {
        public float normalCloudProbability;
        public float denseCloudProbability;
        public float chemicalCloudProbability;
        public float stormCloudProbability;

        public float minSpawnInterval;
        public float maxSpawnInterval;
        public float levelDuration;
    }
    public LevelDifficulty GetLevelDifficulty(int level)
    {
        return levelDifficulties[level];
    }
}

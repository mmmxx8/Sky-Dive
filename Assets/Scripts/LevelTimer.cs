using UnityEngine;

public static class LevelTimer
{
    public static float GetTimePassed()
    {
        return Time.timeSinceLevelLoad;
    }

    public static void SetPaused(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
    }
}

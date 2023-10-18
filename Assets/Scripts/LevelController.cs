using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int nextLevelIndex = 1;
    private Enemy[] enemies;

    private void OnEnable()
    {
        enemies = FindObjectsOfType<Enemy>();
    }

    void Update()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
                return;
        }

        NextLevel();
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(nextLevelIndex++);
    }
}

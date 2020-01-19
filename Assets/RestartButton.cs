using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private static void Restart()
    {
        SceneManager.LoadScene(GameManager.Instance.currentLevelScene);
        LevelManager.Instance.GenerateBullets(true);
        Debug.Log("Restarted | " + LevelManager.Instance.bulletQueue.Count);
    }

    private void Update()
    {
        if(Input.anyKeyDown)
            Restart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void Play(string startScene = "Scenes/Level 1")
    {
        SceneManager.LoadScene(startScene);
    }
}

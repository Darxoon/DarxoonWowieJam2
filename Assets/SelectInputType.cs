using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectInputType : MonoBehaviour
{
    [SerializeField] private string startingScene;

    private void Start()
    {
        Debug.Log("hi");
    }

    public void SelectType(string type)
    {
        GameManager.Instance.inputType = type == "Gamepad" ? InputType.Gamepad : InputType.KeyboardMouse;
        SceneManager.LoadScene(startingScene);
    }
}

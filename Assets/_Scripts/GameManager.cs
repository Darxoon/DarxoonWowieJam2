using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputType inputType;

    public Vector2 movementAxis;
    public Vector2 aimAxis;
    public Quaternion bulletRotation;
    
    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        
    }
}

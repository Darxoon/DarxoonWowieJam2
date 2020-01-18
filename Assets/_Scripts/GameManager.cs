using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputType inputType;

    public Vector2 movementAxis;
    public Vector2 aimAxis;

    public Queue<GameObject> bulletQueue = new Queue<GameObject>();

    public float bulletSpeed;

    [Header("References")] 
    
    public CompositeCollider2D mapCollider;
    
    [Header("Starting Information")] 
    
    [SerializeField] private int bulletAmount;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletHeader;
    
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
        for(int i = 0; i < bulletAmount; i++)
        {
            GameObject instance = Instantiate(bulletPrefab, new Vector3(), Quaternion.identity, bulletHeader);
            bulletQueue.Enqueue(instance);
            instance.SetActive(false);
        }
    }

}

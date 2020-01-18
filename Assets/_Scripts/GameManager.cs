using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputType inputType;

    public Vector2 movementAxis;
    public Vector2 aimAxis;

    public float bulletSpeed;

    [Header("State")] 
    
    public bool usingGlobalBulletDirection = false;

    [Header("Starting Information")] 
    
    public int bulletAmount;
    public GameObject bulletPrefab;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}

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
    
    [Header("Screenshake")]
    
    public float shootScreenShake = 0.1f;
    public float hitScreenShake = 0.3f;
    public float killScreenShake = 1f;
    public float playerHitScreenShake = 0.6f;

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

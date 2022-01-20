using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputType inputType;

    public Vector2 movementAxis;
    public Vector2 aimAxis;

    public float bulletSpeed;

    public string currentLevelScene = "Scenes/Main";
    
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

    [Header("Sounds")] 
    
    [SerializeField] private SoundItem[] inspectorSounds;
    [HideInInspector] public Dictionary<string, AudioClip[]> sounds = new Dictionary<string, AudioClip[]>();

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        foreach (SoundItem soundItem in inspectorSounds)
        {
//            Debug.Log(soundItem);
//            Debug.Log(soundItem.Name);
//            Debug.Log(soundItem.Sounds);
            sounds.Add(soundItem.Name, soundItem.Sounds);
            Debug.Log(soundItem.Sounds.Length);
            Debug.Log(sounds[soundItem.Name].Length);
            Debug.Log(soundItem.Name);
        }
    }

    public AudioClip GetClip(string clipName)
    {
//        Debug.LogWarning("wtf");
//        Debug.Log(clipName);
//        Debug.Log(sounds);
        int element = Random.Range(0, sounds[clipName].Length);
//        Debug.Log(element);
//        Debug.Log(sounds.Count);
        return sounds[clipName][element];
    }
}

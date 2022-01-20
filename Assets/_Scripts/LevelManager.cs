using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public CompositeCollider2D mapCollider;

    public Transform bulletHeader;

    [SerializeField] private Transform enemiesHeaderTransform;
    
    public Queue<GameObject> bulletQueue = new Queue<GameObject>();

    public int requiredKills;
    public GameObject goalToAppear;
    public GameObject goalToDisappear;
    public bool resetIfChanged;

    [SerializeField] private bool useGlobalBulletDirection;
    
    
    
    private void Awake()
    {
        if(Instance)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    private void Start()
    {
        GenerateBullets(false);
        GameManager.Instance.currentLevelScene = SceneManager.GetActiveScene().name;
        GameManager.Instance.usingGlobalBulletDirection = useGlobalBulletDirection;
    }

    public void GenerateBullets(bool destroyPrevious)
    {
        if (destroyPrevious)
        {
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (GameObject bullet in bulletQueue)
            {
                if(bullet)
                    Destroy(bullet);
            }
            bulletQueue.Clear();
        }

        for(int i = 0; i < GameManager.Instance.bulletAmount; i++)
        {
            GameObject instance = Instantiate(GameManager.Instance.bulletPrefab, new Vector3(), Quaternion.identity, LevelManager.Instance.bulletHeader);
            bulletQueue.Enqueue(instance);
            instance.GetComponent<Bullet>().SetActive(false);
        }
    }

    public void RecalculateGoalRequirement(int modifier)
    {
        Debug.LogWarning(modifier);
        Debug.LogWarning(requiredKills);
        requiredKills += modifier;
        if (requiredKills <= 0)
        {
            if(goalToAppear)
                goalToAppear.SetActive(true);
            if(goalToDisappear)
                goalToDisappear.SetActive(false);
        }
        else if(resetIfChanged)
        {
            if(goalToAppear)
                goalToAppear.SetActive(false);
            if(goalToDisappear)
                goalToDisappear.SetActive(true);
        }
    }
}

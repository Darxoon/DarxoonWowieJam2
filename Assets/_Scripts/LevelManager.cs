using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public CompositeCollider2D mapCollider;

    public Transform bulletHeader;
    
    
    public Queue<GameObject> bulletQueue = new Queue<GameObject>();
    
    private void Awake()
    {
        if(Instance)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    private void Start()
    {
        GenerateBullets(false);
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
            instance.SetActive(false);
        }
    }
}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    
    [SerializeField] private Camera mainCam;
    
    [SerializeField] private Vector3 offset;
    [SerializeField] [Range(0, 1)] private float drag = 1;

    public float shake;
    public float shakeAmount = 0.7f;
    public float shakeDecreaseFactor = 1f;

    private void Awake() 
    {
        if(Instance)
            Destroy(Instance);
        Instance = this;
    }

    private void Update()
    {
        if (shake > 0)
        {
            Vector2 rawNewLocation = shake * shakeAmount * shake * Random.insideUnitCircle;
            Vector3 localPosition = mainCam.transform.localPosition;
            localPosition =  Vector3.Lerp(localPosition, new Vector3(rawNewLocation.x, rawNewLocation.y, localPosition.z), .7f);
            mainCam.transform.localPosition = localPosition;
            shake -= Time.deltaTime * shakeDecreaseFactor;
        }
        else
            shake = 0;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        Vector3 goalOffset = Player.Instance.transform.position + offset - position;
        position = Vector3.Lerp(position, goalOffset + position, drag);
        transform.position = position;
    }

    public void Shake(float amount)
    {
        shake += Mathf.Sqrt(shake + amount) * Time.deltaTime;
    }
    
}

using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Vector3 offset;
    [SerializeField] [Range(0, 1)] private float drag = 1;
    [SerializeField] private float moveTooMuch = 1.2f;
    
    [Header("References")]
    
    [SerializeField] private Player player;
    [SerializeField] private Camera mainCam;

    private void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        Vector3 goalOffset = player.transform.position + offset - position;
        position = Vector3.Lerp(position, goalOffset + position, drag);
        transform.position = position;
    }
}

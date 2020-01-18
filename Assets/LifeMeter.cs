using System;
using UnityEngine;
using TMPro;

public class LifeMeter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = $"Life: {Player.Instance.health}";
    }
}

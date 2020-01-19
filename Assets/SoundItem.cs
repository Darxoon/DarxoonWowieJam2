using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "UnassignedField.Global")]
[Serializable]
public struct SoundItem
{
    public string Name;
    public AudioClip[] Sounds;
}
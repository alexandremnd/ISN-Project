using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Weather Preset", menuName = "Game/Weather Preset")]
public class WeatherPreset : ScriptableObject
{
    [Header("Weather parameters preset")]
    public Weather[] weather;
}
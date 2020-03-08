using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weather
{
    [Header("Weather Settings")]
    public string weatherName;

    [Header("Altitude Settings")]
    public float minimalAltitude;
    public float maximalAltitude;

    [Header("Cloud Size")]
    public float minimalSize;
    public float maximalSize;

    [Header("Cloud type")]
    [EnumFlags]
    public CloudType clouds;

    [Header("Weather events probabilty")]
    public float rainProbability;
    public float stormProbability;
}
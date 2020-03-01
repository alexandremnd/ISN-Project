using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cloud Data", menuName = "Game/Cloud Data")]
public class CloudsData : ScriptableObject
{
    [Header("Clouds")]
    public GameObject[] clouds;
}

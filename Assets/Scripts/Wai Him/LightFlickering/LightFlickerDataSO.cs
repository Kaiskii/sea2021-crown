using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Light Flicker Data", menuName = "ScriptableObjects/Light Flicker Data", order = 1)]
public class LightFlickerDataSO : ScriptableObject
{
    public float minInt;
    public float maxInt;

    public float lerpDuration = 3;
    public float flickerTime = 1.2f;
}

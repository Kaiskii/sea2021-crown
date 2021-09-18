using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tween", menuName = "ScriptableObjects/Player Tween", order = 1)]
public class PlayerTweenSO : ScriptableObject
{
  [Header("Idle Scale Settings")]
  public Vector3 endSize;
  public float scaleSpeed = 3.0f;
  public float waitTime = 0.2f;
  public AnimationCurve animCurv;
}

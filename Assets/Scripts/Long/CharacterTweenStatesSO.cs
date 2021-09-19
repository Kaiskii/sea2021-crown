using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New State List", menuName = "ScriptableObjects/Tween State List", order = 1)]
public class CharacterTweenStatesSO : ScriptableObject
{
  public List<int> stateTweenIDs;
  public List<float> crownOffsets;

  public RuntimeAnimatorController gooController;
  public int gooTweenID = 2;
}

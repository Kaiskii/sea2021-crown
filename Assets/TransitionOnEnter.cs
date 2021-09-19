using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TransitionOnEnter : MonoBehaviour
{
  [SerializeField] string levelName;
  [SerializeField] LevelLoader loader;

  void OnTriggerEnter2D(Collider2D other)
  {        
    if (other.CompareTag("Pawn")) {
      Debug.Log("Transition!");
      loader.LoadLevel(levelName);
    }
  }
}

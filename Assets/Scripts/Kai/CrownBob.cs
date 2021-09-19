using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownBob : MonoBehaviour {
  [SerializeField] Transform attachedPlayer;
  [SerializeField] float yOffset = 1.4f;
  float baseOffset = 0f;

  void Update() {
    this.transform.localPosition = new Vector2(attachedPlayer.localPosition.x, attachedPlayer.localPosition.y + attachedPlayer.localScale.y - 1.0f + yOffset+baseOffset);
  }

  public void SetOffset(float offset){
    baseOffset = offset;
  }

  public void SetYOffset(float offset){
    yOffset = offset;
    baseOffset = 0;
  }
}

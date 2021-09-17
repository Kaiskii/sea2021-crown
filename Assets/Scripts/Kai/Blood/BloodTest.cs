using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTest : MonoBehaviour {
  [SerializeField] GameObject blood;
  [SerializeField] Transform bloodHolder;

  Vector3 worldPosition;

  void Update() {
    if (Input.GetKeyDown(KeyCode.B)) {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = Camera.main.nearClipPlane;
      worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
      Instantiate(blood, new Vector3(worldPosition.x, worldPosition.y, 0.0f), Quaternion.identity, bloodHolder);
    }
  }
}

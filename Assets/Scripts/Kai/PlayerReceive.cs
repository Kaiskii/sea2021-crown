using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerReceive : MonoBehaviour {
  CinemachineVirtualCamera cvc;

  void Start() {
    cvc = GameObject.FindGameObjectWithTag("CloseCamera").GetComponent<CinemachineVirtualCamera>();
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("CrownPhysics") && ActivePlayerManager.Instance.ActivePlayer != this.gameObject) {
      Destroy(other.gameObject);

      cvc.m_Follow = this.transform;

      // Setting Properties to become Player
      foreach (Transform tr in this.transform.parent) {
        tr.gameObject.layer = LayerMask.NameToLayer("Player");
      }

      this.GetComponent<CharacterMovement>().enabled = true;
      this.GetComponent<PlayerThrow>().enabled = true;

      ActivePlayerManager.Instance.ActivePlayer = this.gameObject;

      this.enabled = false;
    }
  }
}

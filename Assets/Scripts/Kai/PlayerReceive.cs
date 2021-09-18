using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerReceive : MonoBehaviour {
  [SerializeField] GameObject visualCrown;

  CinemachineVirtualCamera cvc;

  void Start() {
    cvc = GameObject.FindGameObjectWithTag("CloseCamera").GetComponent<CinemachineVirtualCamera>();
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (ActivePlayerManager.Instance.CanRecieveCrown(this.gameObject) && other.CompareTag("CrownPhysics")) {
      Destroy(other.gameObject);

      ActivePlayerManager.Instance.CrownCaught(this.gameObject);

      cvc.m_Follow = this.transform;

      this.GetComponent<CharacterMovement>().enabled = true;
      visualCrown.SetActive(true);
    }
  }
}

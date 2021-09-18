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
    if (other.CompareTag("CrownPhysics") && ActivePlayerManager.Instance.ActivePlayer != this.gameObject) {
      Destroy(other.gameObject);

      cvc.m_Follow = this.transform;

      this.GetComponent<CharacterMovement>().enabled = true;
      this.GetComponent<PlayerThrow>().enabled = true;
      visualCrown.SetActive(true);

      ActivePlayerManager.Instance.ActivePlayer = this.gameObject;

      this.enabled = false;
    }
  }
}

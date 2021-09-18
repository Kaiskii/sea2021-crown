using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownLeavesPlayer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
    if (other.CompareTag("CrownPhysics") && ActivePlayerManager.Instance.ActivePlayer == this.gameObject) {
      ActivePlayerManager.Instance.ActivePlayer = null;
    }
  }
}

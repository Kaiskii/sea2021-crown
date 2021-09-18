using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerManager : MonoBehaviour {
  private static ActivePlayerManager _instance;

  public static ActivePlayerManager Instance { get { return _instance; } }

  public GameObject activePlayer;

  private void Awake() {
    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
    } else {
      _instance = this;
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerManager : MonoBehaviour {
  private static ActivePlayerManager _instance;

  public static ActivePlayerManager Instance { get { return _instance; } }

  [SerializeField] GameObject activePlayer = null;
  [SerializeField] GameObject lastActivePlayer = null;
  [SerializeField] LevelLoader loader;
  [SerializeField] GameObject resetPrompt;

  public bool canPickUpOwnCrown = true;
  public bool shouldReset = false;

  [SerializeField]float crownCooldownDuration = 0.5f;
  [SerializeField]float resetPromptDuration = 10f;

  float elapsedTime;
  float resetTime;

  void Update()
  {
    if(!canPickUpOwnCrown){
      elapsedTime += Time.deltaTime;
      if(elapsedTime >= crownCooldownDuration){
        canPickUpOwnCrown = true;
        lastActivePlayer = null;
        elapsedTime = 0;
      }
    }

    if(activePlayer == null){
      resetTime += Time.deltaTime;
      if(resetTime >= resetPromptDuration){
        shouldReset = true;
        resetTime = 0;
      }
    } else {
      shouldReset = false;
    }

    if(Input.GetKeyDown(KeyCode.R) && loader){
      loader.ReloadLevel();
    }

    if(Input.GetKeyDown(KeyCode.Escape) && loader){
      loader.ReturnToMenu();
    }

    if(resetPrompt) resetPrompt.SetActive(shouldReset);
  }

  public bool CanRecieveCrown(GameObject me){
    return (activePlayer == null && me != lastActivePlayer);
  }

  public bool CanThrowCrown(GameObject me){
    return (me == activePlayer);
  }

  public void CrownThrown(){
    SoundManager.Instance.PlayRandomPitch("CrownThrow");

    canPickUpOwnCrown = false;
    //Record the person who threw it, they can't pick it back up till after cooldown
    lastActivePlayer = activePlayer;
    activePlayer = null;
  }

  public void CrownCaught(GameObject me){
    activePlayer = me;
  }

  private void Awake() {
    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
    } else {
      _instance = this;
    }
  }
}

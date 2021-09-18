using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerReceive : MonoBehaviour {
  [SerializeField] GameObject visualCrown;

  [SerializeField] List<GameObject> nearbyPawns;
  [SerializeField] GameObject nearestPawn = null;
  float closestDistance = 9999;

  CinemachineVirtualCamera cvc;

  void Start() {
    cvc = GameObject.FindGameObjectWithTag("CloseCamera").GetComponent<CinemachineVirtualCamera>();
    nearbyPawns = new List<GameObject>();
  }

  void Update(){
    if (ActivePlayerManager.Instance.CanThrowCrown(this.gameObject)) {
      if(Input.GetKeyDown(KeyCode.E) && nearestPawn){

        this.GetComponent<CharacterMovement>().enabled = false;
        visualCrown.SetActive(false);
        ActivePlayerManager.Instance.CrownThrown();

        ActivePlayerManager.Instance.CrownCaught(nearestPawn);
        cvc.m_Follow = nearestPawn.transform;
        nearestPawn.GetComponent<CharacterMovement>().enabled = true;
        nearestPawn.GetComponent<PlayerReceive>().visualCrown.SetActive(true);
        nearestPawn.GetComponent<PlayerReceive>().CheckForNearestPawn();
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    //Can we pick up a thrown crown?
    if (ActivePlayerManager.Instance.CanRecieveCrown(this.gameObject) && other.CompareTag("CrownPhysics")) {
      Destroy(other.gameObject);

      ActivePlayerManager.Instance.CrownCaught(this.gameObject);

      cvc.m_Follow = this.transform;

      this.GetComponent<CharacterMovement>().enabled = true;
      visualCrown.SetActive(true);
      CheckForNearestPawn();
    }

    //Can we pass a crown to a nearby pawn?
    if(other.CompareTag("Pawn")){
      nearbyPawns.Add(other.gameObject);
      CheckForNearestPawn();
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if(other.CompareTag("Pawn")){
      if(nearbyPawns.Contains(other.gameObject)){
        nearbyPawns.Remove(other.gameObject);
        CheckForNearestPawn();
      }
    }
  }

  void CheckForNearestPawn(){
    //If we can throw a crown but there's someone nearer, prompt to pass it instead
    if(ActivePlayerManager.Instance.CanThrowCrown(this.gameObject)){
      closestDistance = 9999;
      nearestPawn = null;
      for (int i = 0;i<nearbyPawns.Count;++i){
        if(!nearbyPawns[i]){
          nearbyPawns.Remove(nearbyPawns[i]);
          continue;
        }

        if(Vector3.Distance(this.gameObject.transform.position,nearbyPawns[i].transform.position) < closestDistance){
          nearestPawn = nearbyPawns[i];
        }
      }

      if(nearestPawn){
        WorldUIPrompt.Instance.ShowPrompt(nearestPawn,new Vector3(0,3,0));
      } else {
        WorldUIPrompt.Instance.HidePrompt();
      }
    }
  }
}

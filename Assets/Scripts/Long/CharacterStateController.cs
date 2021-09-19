using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
  [SerializeField] int TweenStateID;
  CharacterTweenStatesSO data;

  int currentState = 0;

  playerIdle idle;
  Animator animator;
  SpriteRenderer rend;
  [SerializeField] CrownBob bob;

  bool canSquish = true;


  void Start()
  {
    data = ResourceIndex.GetAsset<CharacterTweenStatesSO>(TweenStateID);

    idle = GetComponent<playerIdle>();
    animator = GetComponent<Animator>();
    rend = GetComponent<SpriteRenderer>();

    idle.SetTweenID(data.stateTweenIDs[0]);
  }

  public void IncrementCrushState()
  {
    if (!canSquish) return;

    animator.SetBool("IsPanicking",true);
    currentState++;

    if(currentState < data.stateTweenIDs.Count){
      idle.SetTweenID(data.stateTweenIDs[currentState]);
      if(bob) bob.SetOffset(data.crownOffsets[currentState]);
    } else {
      canSquish = false;
      animator.runtimeAnimatorController = data.gooController;
      idle.SetTweenID(data.gooTweenID);
      if(bob) bob.SetOffset(-1.8f);
    }

  }

  public void SetMovementDirection(float dir)
  {
    if(dir == 0)
      animator.SetBool("IsMoving",false);
    else{
      animator.SetBool("IsMoving",true);
      if(dir>0)
        rend.flipX = true;
      else
        rend.flipX = false;
    }

  }
}

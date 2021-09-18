using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
  //[SerializeField] bool isPanicking = false;
  //[SerializeField] int movementDirection = 0;
  //[Space]
  [SerializeField] int panicTweenID = 1;
  [SerializeField] int normalTweenID = 0;

  playerIdle idle;
  Animator animator;
  SpriteRenderer rend;

  void Start()
  {
    idle = GetComponent<playerIdle>();
    animator = GetComponent<Animator>();
    rend = GetComponent<SpriteRenderer>();
  }

  /*
  void Update()
  {
    SetPanicking(isPanicking);
    SetMovementDirection(movementDirection);
  }
  */

  public void SetPanicking(bool state)
  {
    animator.SetBool("IsPanicking",state);

    if(state)
      idle.SetTweenID(panicTweenID);
    else
      idle.SetTweenID(normalTweenID);
  }

  public void SetMovementDirection(int dir)
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

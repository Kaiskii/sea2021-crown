using UnityEngine;
using System;

public class CrownImpactMessager : MonoBehaviour
{
  void OnCollisionEnter2D(Collision2D col)
  {
    SoundManager.Instance?.PlayRandomPitch("CrownLand");
    ScreenShake.Instance?.ShakeCamera(10f,10f,10f);
    CrownEventListener.Instance?.OnCrownImpact(this.gameObject,EventArgs.Empty);
  }
}

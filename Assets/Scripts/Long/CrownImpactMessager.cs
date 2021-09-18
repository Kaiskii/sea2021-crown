using UnityEngine;
using System;

public class CrownImpactMessager : MonoBehaviour
{
  void OnCollisionEnter2D(Collision2D col)
  {
    SoundManager.Instance?.PlayRandomPitch("CrownLand");
    ScreenShake.Instance?.ShakeCamera(0.2f,0.2f,0.2f);
    ParticleManager.Instance?.CreateParticle("CrownPuff",col.contacts[0].point);

    CrownEventListener.Instance?.OnCrownImpact(this.gameObject,EventArgs.Empty);
  }
}

using UnityEngine;
using System;

public class CrownImpactMessager : MonoBehaviour
{
  [SerializeField] string hitSFX = "CrownLand";
  [SerializeField] string hitParticle = "FireLine";

  void OnCollisionEnter2D(Collision2D col)
  {
    SoundManager.Instance?.PlayRandomPitch(hitSFX,0.6f,0.9f);
    ScreenShake.Instance?.ShakeCamera(3f,0.2f,2f);
    ParticleManager.Instance?.CreateParticle(hitParticle,col.GetContact(0).point,col.GetContact(0).normal);
    CrownEventListener.Instance?.OnCrownImpact(this.gameObject,EventArgs.Empty);
  }
}

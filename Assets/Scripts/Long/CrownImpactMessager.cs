using UnityEngine;
using System;

public class CrownImpactMessager : MonoBehaviour
{
  [SerializeField] string hitSFX = "CrownLand";
  [SerializeField] string hitParticle = "FireLine";
  [SerializeField] Vector3 cameraShakeParams = new Vector3(5f,0.2f,3f);

  void OnCollisionEnter2D(Collision2D col)
  {
    SoundManager.Instance?.PlayRandomPitch(hitSFX,0.6f,0.9f);
    ScreenShake.Instance?.ShakeCamera(cameraShakeParams.x,cameraShakeParams.y,cameraShakeParams.z);
    ParticleManager.Instance?.CreateParticle(hitParticle,transform.position,col.GetContact(0).normal);
    CrownEventListener.Instance?.OnCrownImpact(this.gameObject,EventArgs.Empty);
  }
}

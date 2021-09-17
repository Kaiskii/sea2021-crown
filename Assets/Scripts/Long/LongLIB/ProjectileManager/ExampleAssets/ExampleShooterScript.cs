using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REQUIRES RESOURCEINDEX
//Create a new projectile scriptable Create>ScriptableObjects>Projectile
//Assign ExampleBullet and ExampleFX

public class ExampleShooterScript : MonoBehaviour
{
  [SerializeField]
  int projectileID;
  
  [SerializeField]
  GameObject target;

  Vector3 direction;
  float incrementAmount;

  void Start()
  {
    direction = Vector3.up;
    incrementAmount = 0;
  }

  // Update is called once per frame
  void Update()
  {
    if(Input.GetKeyDown(KeyCode.Space))
    {
      Shoot(target);
    }
  }

  void Shoot(GameObject target)
  {
    direction = Quaternion.Euler(0, 0, incrementAmount)*Vector3.up;
    direction.Normalize();
    incrementAmount += 10;

    Debug.DrawLine(transform.position,direction*5f,Color.cyan,0.5f);

    ParticleManager.Instance.CreateParticle("Pew",transform.position,direction);
    SoundManager.Instance.Play("GunFX");

    if(target == null)
      ProjectileManager.Instance.FireProjectile(projectileID,transform.position,direction);
    else
      ProjectileManager.Instance.FireProjectile(projectileID,transform.position,direction,target,new Vector3(0,0,0));
  }
}

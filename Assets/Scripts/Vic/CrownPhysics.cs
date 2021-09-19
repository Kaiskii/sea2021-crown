using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownPhysics : MonoBehaviour
{
    public void Init(Vector2 velocity, float torqueStrength)
    {
        this.GetComponent<Rigidbody2D>().AddTorque(torqueStrength);
		this.GetComponent<Rigidbody2D>().AddForce(velocity);
    }
}

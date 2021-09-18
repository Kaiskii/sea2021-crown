using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Rigidbody2D body;

    [SerializeField]
    private float shakeTimer;

    [SerializeField]
    private float amplitude;

    [SerializeField]
    private float frequency;

    public ParticleSystem dust;
    public ParticleSystem explode;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        speed = 10;
    }

    private void Update() 
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space)) {
            createDust();
            createExplode();
            body.velocity = new Vector2(body.velocity.x, speed);
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            ScreenShake.Instance.ShakeCamera(amplitude, shakeTimer, frequency);
        }
            
    }

    void createDust()
    {
        dust.Play();
    }

    void createExplode()
    {
        explode.Play();
    }


}

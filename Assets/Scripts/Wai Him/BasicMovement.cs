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

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        speed = 10;
    }

    private void Update() 
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
            body.velocity = new Vector2(body.velocity.x, speed);

        if (Input.GetKeyDown(KeyCode.G)) {
            ScreenShake.Instance.ShakeCamera(amplitude, shakeTimer, frequency);
        }
            
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //[SerializeField]
    Rigidbody2D characterRb;

    [SerializeField]
    bool active = true;

    [SerializeField]
    float maxMoveSpeed = 10f;
    [SerializeField]
    float moveAccel = 2f;

    [SerializeField]
    float gravityFactor = 2.5f;

    [SerializeField]
    int crushStates = 4;
    int curCrushState = 3;

    [SerializeField]
    float coyoteTime = 0.3f;
    float coyoteTimer = 0f;
    [SerializeField]
    float jumpBufferTime = 0.1f;
    float jumpBufferTimer = 0f;
    [SerializeField]
    float[] jumpPower = {0f, 5f, 10f, 15f};

    [SerializeField]
    float[] crushHeights = {0.1f, 0.4f, 0.7f, 1f};

    float curMoveSpeed = 0f;

    bool grounded = false;


    void Start()
    {
        characterRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        doMovement();
        doJump();
        doGravity();
    }

    void doMovement()
    {
        /**
        if(Input.GetAxis("Horizontal") != 0)
        {
            curMoveSpeed += Input.GetAxis("Horizontal")*moveAccel*Time.deltaTime;
            curMoveSpeed = Mathf.Clamp(curMoveSpeed, -maxMoveSpeed, maxMoveSpeed);
        }
        else
        {
            curMoveSpeed
        }**/

        curMoveSpeed = Input.GetAxis("Horizontal")*maxMoveSpeed;
        characterRb.velocity = new Vector2(curMoveSpeed, characterRb.velocity.y);
    }

    void doJump()
    {
        if(grounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if(jumpBufferTimer >= 0f && coyoteTimer > 0f)
        {
            characterRb.velocity += new Vector2(0f, jumpPower[curCrushState]);
            jumpBufferTimer = 0f;
        }
    }

    void OnCollisionStay2D(Collision2D other) 
    {
        foreach (ContactPoint2D contact in other.contacts)
        {
			if(contact.normal.y > 0.6f)
			{
				grounded = true;
                break;
			}
        }
    }

    void OnCollisionExit2D(Collision2D contact) 
    {
        grounded = false;
    }

    void doGravity()
    {
        if(!grounded)
        {
            characterRb.velocity += new Vector2(0f, Physics2D.gravity.y * Time.deltaTime);
        }

        if(characterRb.velocity.y <= 5)
        {
            characterRb.velocity += new Vector2(0f, (gravityFactor-1) * Physics2D.gravity.y * Time.deltaTime);
        }
    }
}


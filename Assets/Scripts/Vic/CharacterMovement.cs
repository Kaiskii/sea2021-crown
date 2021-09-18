using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	//[SerializeField]
	Rigidbody2D characterRb;
	BoxCollider2D characterCollider;


	[SerializeField]
	bool active = true;

	[SerializeField]
	float maxMoveSpeed = 10f;

	/**
	[SerializeField]
	float moveAccel = 20f;
	float dragStep = 0f;
	[SerializeField]
	float dragStepRate = 20f;
	**/
	[SerializeField]
	float gravityFactor = 2.5f;

	[SerializeField]
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
	float[] colliderHeight = {0.1f, 0.4f, 0.7f, 1f};

	float curMoveSpeed = 0f;

	bool grounded = false;

	bool willBeCrushed = false;


	void Start()
	{
		characterRb = GetComponent<Rigidbody2D>();
		characterCollider = GetComponent<BoxCollider2D>();
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
		if(active)
		{
			if(Input.GetAxis("Horizontal") != 0)
			{
				/**
				curMoveSpeed += Input.GetAxis("Horizontal")*moveAccel*Time.deltaTime;
				curMoveSpeed = Mathf.Clamp(curMoveSpeed, -maxMoveSpeed, maxMoveSpeed);
				dragStep = 0f;
				**/
				if(Input.GetAxis("Horizontal") > 0)
				{
					curMoveSpeed =  maxMoveSpeed * Time.deltaTime;
				}
				else
				{
					curMoveSpeed =  -maxMoveSpeed * Time.deltaTime;
				}
			}
			else
			{
				/**
				if(dragStep < 1)
				{
					dragStep += dragStepRate * Time.deltaTime;
				}
				else
				{
					dragStep = 1f;
				}
				curMoveSpeed = Mathf.Lerp(curMoveSpeed, 0, dragStep);
				**/
				curMoveSpeed = 0;
			}
		}
		else
		{
			curMoveSpeed = 0;
		}

		curMoveSpeed = Input.GetAxis("Horizontal")*maxMoveSpeed;
		characterRb.velocity = new Vector2(curMoveSpeed, characterRb.velocity.y);
	}

	void doJump()
	{
		if(active)
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
				willBeCrushed = true;
				jumpBufferTimer = 0f;
			}
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

	void OnCollisionEnter2D(Collision2D other) 
	{
		foreach (ContactPoint2D contact in other.contacts)
		{
			if(willBeCrushed && contact.normal.y > 0.6f)
			{
				crushCharacter();
				willBeCrushed = false;
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

	public void crushCharacter()
	{
		if(curCrushState > 0)
		{
			curCrushState -= 1;
		}
		characterCollider.size = new Vector2 (characterCollider.size.x, colliderHeight[curCrushState]);
		
	}
}


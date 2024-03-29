using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	//[SerializeField]
	Rigidbody2D characterRb;
	BoxCollider2D characterCollider;
	CharacterStateController characterState;

	[SerializeField]
	public bool active = true;

	
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
	public int curCrushState = 3;
  [SerializeField] bool ignoreFirstDrop = true;

	[SerializeField]
	float coyoteTime = 0.3f;
	float coyoteTimer = 0f;
	[SerializeField]
	float jumpBufferTime = 0.1f;
	float jumpBufferTimer = 0f;
	[SerializeField]
	float[] jumpPower = {0f, 5f, 10f, 15f};

	[SerializeField]
	float[] colliderHeight = {0.2f, 0.8f, 1.4f, 2f};


	bool willBeCrushed = false;

  	[SerializeField]
  	float walkCycleDelay = 0.5f;
  	[SerializeField]
  	string walkSoundName = "WalkPeasant";
  	[SerializeField]
  	string walkParticleName = "WalkDust";
  	[SerializeField]
  	string jumpParticleName = "CrownPuff";
  	[SerializeField]
  	string jumpSoundName = "JumpPeasant";
    [SerializeField]
  	string crushParticleName = "Squish";
  	[SerializeField]
  	string crushSoundName = "CrownCrunch";
  	[SerializeField]
  	string crushSoundName2 = "CrownSquash1";

  	float elapsedTime = 0;

	float curMoveSpeed = 0f;

	bool grounded = false;


	void Start()
	{
		characterRb = GetComponent<Rigidbody2D>();
		characterCollider = GetComponent<BoxCollider2D>();
		characterState = GetComponent<CharacterStateController>();
	}


	// Update is called once per frame
	void Update()
	{
		doMovement();
		doJump();
		doGravity();
		doFX();
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

			if(Input.GetButtonDown("Jump") && curCrushState > 0)
			{
				jumpBufferTimer = jumpBufferTime;
			}
			else
			{
				jumpBufferTimer -= Time.deltaTime;
			}

			if(jumpBufferTimer >= 0f && coyoteTimer > 0f && grounded)
			{
				characterRb.velocity += new Vector2(0f, jumpPower[curCrushState]);

				jumpBufferTimer = 0f;

        willBeCrushed = true;
				SoundManager.Instance?.Play(jumpSoundName);
				ParticleManager.Instance.CreateParticle(jumpParticleName,transform.position);
			}
		}
	}

  void doFX()
  {
	elapsedTime += Time.deltaTime;
	if(grounded && curMoveSpeed!=0 &&elapsedTime >= walkCycleDelay)
	{
	  elapsedTime = 0;
	  SoundManager.Instance?.Play(walkSoundName);
	  ParticleManager.Instance.CreateParticle(walkParticleName,transform.position);
	}
	characterState.SetMovementDirection(curMoveSpeed);
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

		if(coyoteTimer <= 0f)
		{
			if(!willBeCrushed) willBeCrushed = true;
		}
	}

	public void crushCharacter()
	{
    if(ignoreFirstDrop){
      ignoreFirstDrop = false;
      return;
    }

		if(curCrushState > 0 ){
			curCrushState -= 1;
		}

    if(!characterCollider) characterCollider = GetComponent<BoxCollider2D>();
		characterCollider.size = new Vector2 (characterCollider.size.x, colliderHeight[curCrushState]);
		characterCollider.offset = new Vector2 (characterCollider.offset.x, colliderHeight[curCrushState]/2);

    SoundManager.Instance?.Play(crushSoundName);
    if(curCrushState != 0)
    {
      SoundManager.Instance?.Play(crushSoundName2);
    }
    else
    {
      SoundManager.Instance?.Play("CrownSquash2");
    }

    if(!characterState) characterState = GetComponent<CharacterStateController>();
    characterState.IncrementCrushState();
    ParticleManager.Instance.CreateParticle(crushParticleName,transform.position);
	}
}


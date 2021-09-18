using UnityEngine;
using Cinemachine;

public class PlayerThrow : MonoBehaviour {

  [SerializeField] GameObject crown;
  [SerializeField] GameObject visualCrown;

  GameObject physicsCrown;

  [SerializeField] bool isMouseHold = false;

  [SerializeField] float torqueStrength = 20.0f;
  [SerializeField] float mouseStrength = 10.0f;
  [SerializeField] float mouseHoldTime = 0.2f;
  [SerializeField] float mouseHoldTimer;

  [SerializeField] Vector2 clampStrength = new Vector2(10.0f, 10.0f);

  [SerializeField] Vector3 originalTransformPos;

  CinemachineVirtualCamera cvc;

  Vector3 mouseStartPos;
  Vector3 worldPosition;

  void Start() {
    mouseHoldTimer = mouseHoldTime;
    cvc = GameObject.FindGameObjectWithTag("CloseCamera").GetComponent<CinemachineVirtualCamera>();
  }

  void Update() {
    DoMouseButton();
    DoMouseButtonUp();

    if (physicsCrown && isMouseHold) {
      physicsCrown.transform.position = this.transform.position;
    }
  }

  Vector2 CalculateMousePosition2D() {
    Vector3 mousePos = Input.mousePosition;
    mousePos.z = Camera.main.nearClipPlane;
    return Camera.main.ScreenToWorldPoint(mousePos);
  }

  void DoMouseButton() {
    if (Input.GetMouseButton(0)) {
      if (!isMouseHold) {
        if (mouseHoldTimer <= 0.0f) {
          visualCrown.SetActive(false);
          isMouseHold = true;
          mouseStartPos = CalculateMousePosition2D();
          physicsCrown = Instantiate(crown, this.transform.position, Quaternion.identity);
          physicsCrown.layer = LayerMask.NameToLayer("VisualCrown");
          physicsCrown.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
          originalTransformPos = this.transform.position;
        }

        mouseHoldTimer -= Time.deltaTime;
        return;
      }
    }
  }

  void DoMouseButtonUp() {
    if (Input.GetMouseButtonUp(0)) {
      if (isMouseHold) {
        worldPosition = CalculateMousePosition2D();

        cvc.m_Follow = physicsCrown.transform;

        physicsCrown.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        Vector2 normalizedVector = this.transform.position - originalTransformPos + mouseStartPos - worldPosition;

        Vector2 clamp = new Vector2(
          Mathf.Clamp(normalizedVector.x, -clampStrength.x, clampStrength.x),
          Mathf.Clamp(normalizedVector.y, -clampStrength.y, clampStrength.y)
        );

        Vector2 multipliedVector = clamp * 10.0f * mouseStrength;

        physicsCrown.GetComponent<Rigidbody2D>().AddTorque(torqueStrength);
        physicsCrown.GetComponent<Rigidbody2D>().AddForce(multipliedVector);
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Setting Properties to become an NPC
        this.GetComponent<PlayerReceive>().enabled = true;
        this.GetComponent<CharacterMovement>().enabled = false;
        this.enabled = false;

        physicsCrown.layer = LayerMask.NameToLayer("CrownProjectile");
      }

      mouseHoldTimer = mouseHoldTime;
      isMouseHold = false;
    }
  }
}

using UnityEngine;

public class PlayerThrow : MonoBehaviour {

  [SerializeField] GameObject crown;
  [SerializeField] bool isMouseHold = false;

  [SerializeField] float mouseStrength = 10.0f;
  [SerializeField] float mouseHoldTime = 0.2f;
  [SerializeField] float mouseHoldTimer;

  [SerializeField] Vector2 clampStrength = new Vector2(10.0f, 10.0f);

  Vector3 mouseStartPos;
  Vector3 worldPosition;

  void Start() {
    mouseHoldTimer = mouseHoldTime;
  }

  void Update() {
    DoMouseButton();
    DoMouseButtonUp();
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
          isMouseHold = true;
          mouseStartPos = CalculateMousePosition2D();
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

        var crownGO = Instantiate(crown, this.transform.localPosition, Quaternion.identity);

        Vector2 normalizedVector = mouseStartPos - worldPosition;

        Vector2 clamp = new Vector2(
          Mathf.Clamp(normalizedVector.x, -clampStrength.x, clampStrength.x),
          Mathf.Clamp(normalizedVector.y, -clampStrength.y, clampStrength.y)
        );

        Vector2 multipliedVector = clamp * 10.0f * mouseStrength;

        crownGO.GetComponent<Rigidbody2D>().AddForce(multipliedVector);
      }

      mouseHoldTimer = mouseHoldTime;
      isMouseHold = false;
    }
  }
}

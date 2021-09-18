using UnityEngine;

public class Blood : MonoBehaviour {
  [SerializeField] SpriteRenderer rend;
  [SerializeField] Sprite[] blood;

  [SerializeField] Vector3 maxSize;

  void Start() {
    rend = this.GetComponent<SpriteRenderer>();
    int rand = Random.Range(0, blood.Length);

    this.transform.localScale = new Vector2(
      Random.Range(this.transform.localScale.x, maxSize.x),
      Random.Range(this.transform.localScale.y, maxSize.y)
    );

    rend.sprite = blood[rand];
  }
}

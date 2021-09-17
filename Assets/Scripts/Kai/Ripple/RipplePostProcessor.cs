using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePostProcessor : MonoBehaviour {
    public Material RippleMaterial;
    public float maxAmount = 50f;

    public float friction = 10.0f;

    private float amount = 0f;

    void Update() {
      if (Input.GetKeyDown(KeyCode.R)) {
        StartCoroutine(RippleEffect());
      }

      if (this.amount >= 0.0f) {
        this.RippleMaterial.SetFloat("_Amount", this.amount);
        this.amount -= this.friction * Time.deltaTime;
      }
    }

    IEnumerator RippleEffect () {
      float time = 0.0f;

      while (this.amount != maxAmount) {
        time += Time.deltaTime;
        this.amount = Mathf.Lerp(0, maxAmount, time);
        Vector3 pos = Input.mousePosition;
        this.RippleMaterial.SetFloat("_CenterX", pos.x);
        this.RippleMaterial.SetFloat("_CenterY", pos.y);
      }

      yield return null;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
      Graphics.Blit(src, dst, this.RippleMaterial);
    }
}

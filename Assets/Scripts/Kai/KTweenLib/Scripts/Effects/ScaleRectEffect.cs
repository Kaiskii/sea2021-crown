using System.Collections;
using UnityEngine;

namespace KTweenLib.Scripts.Effects {
  [System.Serializable]
  public class ScaleRectEffect : IUIEffect {
    [SerializeField]
    RectTransform rectTransform { get; }

    [SerializeField]
    Vector3 maxSize { get; }

    [SerializeField]
    float scaleSpeed { get; }

    [SerializeField]
    YieldInstruction wait { get; }

    [SerializeField]
    AnimationCurve animCurve { get; }

    [SerializeField]
    bool pingPong { get; }

    public ScaleRectEffect (RectTransform rectTransform, Vector3 maxSize, float scaleSpeed, YieldInstruction wait, AnimationCurve animCurve, bool pingPong) {
      this.rectTransform = rectTransform;
      this.maxSize = maxSize;
      this.scaleSpeed = scaleSpeed;
      this.wait = wait;
      this.pingPong = pingPong;
      this.animCurve = animCurve;
    }

    override public IEnumerator Execute() {
      float time = 0.0f;
      Vector3 startingScale = this.rectTransform.localScale;
      Vector3 currentScale = this.rectTransform.localScale;

      while (rectTransform.localScale != maxSize) {
        time += Time.deltaTime * scaleSpeed;
        Vector3 scale = Vector3.Lerp(currentScale, maxSize, animCurve.Evaluate(time));
        this.rectTransform.localScale = scale;
        yield return null;
      }

      if (pingPong) {
        yield return wait;

        time = 0.0f;
        currentScale = this.rectTransform.localScale;

        while (this.rectTransform.localScale != startingScale) {
          time += Time.deltaTime * scaleSpeed;
          Vector3 scale = Vector3.Lerp(currentScale, startingScale, animCurve.Evaluate(time));
          this.rectTransform.localScale = scale;
          yield return null;
        }
      }
    }
  }
}

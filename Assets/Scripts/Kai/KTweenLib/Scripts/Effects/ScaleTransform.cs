using System.Collections;
using UnityEngine;

namespace KTweenLib.Scripts.Effects {
  [System.Serializable]
  public class ScaleTransform : IUIEffect {
    [SerializeField]
    Transform _transform { get; }

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

    [SerializeField]
    bool loop { get; }

    public ScaleTransform (
      Transform _transform,
      Vector3 maxSize,
      float scaleSpeed,
      YieldInstruction wait,
      AnimationCurve animCurve,
      bool pingPong,
      bool loop
    ) {
      this._transform = _transform;
      this.maxSize = maxSize;
      this.scaleSpeed = scaleSpeed;
      this.wait = wait;
      this.pingPong = pingPong;
      this.animCurve = animCurve;
      this.loop = loop;
    }

    override public IEnumerator Execute() {
      while (loop) {
        float time = 0.0f;
        Vector3 startingScale = this._transform.localScale;
        Vector3 currentScale = this._transform.localScale;

        while (_transform.localScale != maxSize) {
          time += Time.deltaTime * scaleSpeed;
          Vector3 scale = Vector3.Lerp(currentScale, maxSize, animCurve.Evaluate(time));
          this._transform.localScale = scale;
          yield return null;
        }

        if (pingPong) {
          yield return wait;

          time = 0.0f;
          currentScale = this._transform.localScale;

          while (this._transform.localScale != startingScale) {
            time += Time.deltaTime * scaleSpeed;
            Vector3 scale = Vector3.Lerp(currentScale, startingScale, animCurve.Evaluate(time));
            this._transform.localScale = scale;
            yield return null;
          }
        }
      }
    }
  }
}

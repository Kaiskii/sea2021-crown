using UnityEngine;
using KTweenLib.Scripts;
using KTweenLib.Scripts.Effects;

public class playerIdle : MonoBehaviour {
  EffectBuilder fx;

  [Header("Idle Scale Settings")]
  [SerializeField] Vector3 endSize;
  [SerializeField] float scaleSpeed = 3.0f;
  [SerializeField] float waitTime = 0.2f;
  [SerializeField] AnimationCurve animCurv;

  YieldInstruction _wait;

  private void Awake() {
    _wait = new WaitForSeconds(waitTime);
    fx = new EffectBuilder(this);

    fx.AddEffect(
      new ScaleTransform(this.transform, endSize, scaleSpeed, _wait, animCurv, true, true)
    );
  }

  void Start() {
    fx.ExecuteAllEffects();
  }

  void Update() {

  }
}

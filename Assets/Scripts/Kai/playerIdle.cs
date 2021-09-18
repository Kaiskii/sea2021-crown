using UnityEngine;
using KTweenLib.Scripts;
using KTweenLib.Scripts.Effects;

public class playerIdle : MonoBehaviour {
  EffectBuilder fx;

  [SerializeField] int tweenID = 0;
  PlayerTweenSO tweenData;
  YieldInstruction _wait;

  void Start() {
    fx = new EffectBuilder(this);
    SetTweenID(tweenID);
  }

  public void SetTweenID(int id) {
    tweenID = id;
    tweenData = ResourceIndex.GetAsset<PlayerTweenSO>(tweenID);

    if(!tweenData || fx == null){
      return;
    }

    _wait = new WaitForSeconds(tweenData.waitTime);

    fx.ClearAllEffects();
    fx.AddEffect(
      new ScaleTransform(this.transform, tweenData.endSize, tweenData.scaleSpeed, _wait, tweenData.animCurv, true, true)
    );

    fx.ExecuteAllEffects();
  }
}

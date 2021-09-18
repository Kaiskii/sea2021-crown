using UnityEngine;
using KTweenLib.Scripts;
using KTweenLib.Scripts.Effects;

public class playerIdle : MonoBehaviour {
  EffectBuilder fx;

  [SerializeField] int tweenID = 0;
  PlayerTweenSO tweenData;
  YieldInstruction _wait;

  void Start() {
    SetTweenID(tweenID);
    if(!tweenData){
      return;
    }

    _wait = new WaitForSeconds(tweenData.waitTime);
    fx = new EffectBuilder(this);

    fx.AddEffect(
      new ScaleTransform(this.transform, tweenData.endSize, tweenData.scaleSpeed, _wait, tweenData.animCurv, true, true)
    );

    fx.ExecuteAllEffects();
  }

  public void SetTweenID(int id)
  {
    tweenData = ResourceIndex.GetAsset<PlayerTweenSO>(id);
  }
}

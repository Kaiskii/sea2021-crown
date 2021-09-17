using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTweenLib.Scripts {
  [System.Serializable]
  public class EffectBuilder {
    MonoBehaviour Owner { get; }

    List<IUIEffect> effects = new List<IUIEffect>();

    public EffectBuilder(MonoBehaviour owner) {
      this.Owner = owner;
    }

    public EffectBuilder AddEffect(IUIEffect effect) {
      this.effects.Add(effect);
      return this;
    }

    public void ExecuteAllEffects() {
      this.Owner.StopAllCoroutines();

      foreach (IUIEffect fx in effects) {
        this.Owner.StartCoroutine(fx.Execute());
      }
    }
  }
}

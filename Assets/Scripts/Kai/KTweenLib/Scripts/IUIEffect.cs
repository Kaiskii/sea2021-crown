using System.Collections;
using UnityEngine;

namespace KTweenLib.Scripts {
  public abstract class IUIEffect : MonoBehaviour {
    public abstract IEnumerator Execute();
  }
}

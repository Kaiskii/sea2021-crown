using UnityEngine;
using System;

public class CrownEventListener : Singleton<CrownEventListener>
{
  //EVENTS
  //CrownEventListener.Instance.CrownImpact += [Function];
  public delegate void CrownImpactHandler(GameObject source,EventArgs args);
  public event CrownImpactHandler CrownImpact;
  public void OnCrownImpact(GameObject obj,EventArgs args){
    if(CrownImpact != null){
      CrownImpact(obj,args);
    }
  }
}

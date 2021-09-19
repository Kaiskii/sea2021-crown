using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUIPrompt : Singleton<WorldUIPrompt>
{
  RectTransform rect;
  Image image;

  [SerializeField] GameObject target;
  [SerializeField] Vector3 targetOffset;

  void Start()
  {
    rect = GetComponent<RectTransform>();
    image = GetComponent<Image>();
  }

  // Update is called once per frame
  void Update()
  {
      if(target){
        rect.transform.position = Camera.main.WorldToScreenPoint(target.transform.position+targetOffset);
      }
  }

  public void ShowPrompt(GameObject trackedTarget, Vector3 offset){
    target = trackedTarget;
    targetOffset = offset;
    image.enabled = true;
  }

  public void HidePrompt(){
    target = null;
    image.enabled = false;
  }
}

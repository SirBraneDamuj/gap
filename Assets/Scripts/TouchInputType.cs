using UnityEngine;
using System.Collections;

public class TouchInputType : InputType {
  public bool InputStarted() {
    return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
  }
  
  public bool InputEnded() {
    return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
  }

  public Vector2 CurrentInput() {
    return Input.GetTouch(0).position;
  }
}

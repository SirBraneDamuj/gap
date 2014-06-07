using UnityEngine;
using System.Collections;

public class MouseInputType : InputType {
  public bool InputStarted() {
    return Input.GetMouseButtonDown(0);
  }
  
  public bool InputEnded() {
    return Input.GetMouseButtonUp(0);
  }

  public Vector2 CurrentInput() {
    return Input.mousePosition;
  }
}

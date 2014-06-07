using UnityEngine;
using System.Collections;

public interface InputType {

  bool InputStarted();
  bool InputEnded();
  Vector2 CurrentInput();

}

using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

  void OnMouseDown() {
    Camera.main.SendMessage("StartedDrag");
  }

}

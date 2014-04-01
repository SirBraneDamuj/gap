using UnityEngine;
using System.Collections;

public class MouseFlick : MonoBehaviour {

  public GameObject target;
  
  private bool startedFlick = false;
  private Vector3 startPos;
  private Vector3 endPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if(Input.GetMouseButtonDown(0)) {
      startedFlick = true;
      startPos = Input.mousePosition;
      endPos = Input.mousePosition;
    }
    else if(startedFlick && Input.GetMouseButtonUp(0)) {
      startedFlick = false;
      endPos = Input.mousePosition;
      target.SendMessage("Flick", startPos);
    } else if(startedFlick) {
      endPos = Input.mousePosition;
    }
	}
  
  void OnGUI() {
    if(Event.current.type == EventType.Repaint && startedFlick) {
      Vector2 start = new Vector2(startPos.x, Screen.height - startPos.y);
      Vector2 end = new Vector2(endPos.x, Screen.height - endPos.y);
      Drawing.DrawLine(start, end, Color.red, 2.0f, true);
    }
  }
}

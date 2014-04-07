using UnityEngine;
using System.Collections;

public class MouseFlick : MonoBehaviour {

  public Coin target;
  
  private bool startedFlick = false;
  private Vector3 startPos;
  private Vector3 endPos;
	
	// Update is called once per frame
	void Update () {
    if(startedFlick && Input.GetMouseButtonUp(0)) {
      startedFlick = false;
      endPos = Input.mousePosition;
      target.SendMessage("Flick", startPos);
    } else if(startedFlick) {
      endPos = Input.mousePosition;
    }
	}
  
  void StartedDrag() {
    if(target != null) {
      startedFlick = true;
      startPos = Input.mousePosition;
      endPos = Input.mousePosition;
    }
  }
  
  void SelectedNewCoin(GameObject newTarget) {
    if(target != null) {
      target.Deselect();
    }
    target = newTarget.GetComponent<Coin>();
    target.Select();
  }
  
  void OnGUI() {
    if(Event.current.type == EventType.Repaint && startedFlick) {
      Vector2 p1 = new Vector2(startPos.x, Screen.height - startPos.y);
      Vector2 p2 = new Vector2(endPos.x, Screen.height - endPos.y);
      Vector2 q1 = Camera.main.WorldToScreenPoint(target.transform.position);
      q1.y = Screen.height - q1.y;
      Vector2 q2 = p2 + (q1 - p1);
      
      Drawing.DrawLine(q1, q2, Color.red, 2.0f, true);
    }
  }
}

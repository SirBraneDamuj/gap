using UnityEngine;
using System.Collections;

public class FlickProperties {
  public Vector2 initialPos;
  public Vector2 start;
  public Vector2 end;
  public Coin[] gate;
  public float timer;
  
  public FlickProperties(Vector2 initialPos, Vector2 end, Coin[] gate) {
    this.initialPos = initialPos;
    this.start = Camera.main.WorldToScreenPoint(initialPos);
    this.end = end;
    this.gate = gate;
    this.timer = 0.0f;
  }
  
  public float DetermineGateSide(Vector2 v) {
    Vector2 a = gate[0].transform.position;
    Vector2 b = gate[1].transform.position;
    return DetermineSide(a, b, v);
  }
  
  public bool Contained(Vector2 v) {
    Vector2 a = gate[0].transform.position;
    Vector2 b = Vector3.Project(initialPos, gate[0].transform.position);
    Vector2 c = gate[1].transform.position;
    Vector2 d = Vector3.Project(initialPos, gate[1].transform.position);
    return DetermineSide(a, b, v) != DetermineSide(c, d, v);
  }
  
  private float DetermineSide(Vector2 a, Vector2 b, Vector2 v) {
    return Mathf.Sign((b.x - a.x)*(v.y - a.y) - (b.y - a.y)*(v.x - a.x));
  }
  
}

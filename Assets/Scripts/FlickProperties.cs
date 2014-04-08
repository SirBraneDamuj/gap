using UnityEngine;
using System.Collections;

public class FlickProperties {
  public Vector2 start;
  public Vector2 end;
  public Coin[] gate;
  public float timer;
  
  public FlickProperties(Vector2 start, Vector2 end, Coin[] gate) {
    this.start = start;
    this.end = end;
    this.gate = gate;
    this.timer = 0.0f;
  }
  
  public float DetermineSide(Vector2 v) {
    Vector2 a = gate[0].transform.position;
    Vector2 b = gate[1].transform.position;
    return Mathf.Sign((b.x - a.x)*(v.y - a.y) - (b.y - a.y)*(v.x - a.x));
  }
}

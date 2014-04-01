using UnityEngine;
using System.Collections;

public class FlickReceiver : MonoBehaviour {

  public const float MAX_FLICK_RANGE=200.0f;
  public const float MAX_FORCE = 1000.0f;

  void Flick(Vector3 startPos) {
    Vector2 start = new Vector2(startPos.x, Screen.height - startPos.y);
    Vector2 end = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
    float angle = GetAngle(start, end);
    float power = MAX_FORCE * (Mathf.Min((start-end).magnitude, MAX_FLICK_RANGE)) / MAX_FLICK_RANGE;
    
    Vector2 force = (start-end).normalized * power;
    force.x *= -1.0f;
    
    rigidbody2D.AddForce(force);
  }
  
  private float GetAngle(Vector2 startPos, Vector2 endPos) {
    Vector2 referenceRight= Vector3.Cross(Vector2.up, startPos);
    float angle = Vector2.Angle(endPos, startPos);
    float sign = Mathf.Sign(Vector2.Dot(endPos, referenceRight));
     
    return sign * angle;
  }

}

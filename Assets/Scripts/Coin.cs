using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

  public bool flicked = false;
  public float stoppedVelocity = 0.1f;
  public float delayTimer = 0.3f;
  private FlickProperties flickProperties;
  private float side;
  public const float MAX_FLICK_RANGE=200.0f;
  public const float MAX_FORCE = 1000.0f;
  
  void Update() {
    if(Mathf.Abs(rigidbody2D.velocity.magnitude) <= stoppedVelocity) {
      rigidbody2D.velocity = Vector3.zero;
    }
    if(flicked) {
      flickProperties.timer += Time.deltaTime;
      if(flickProperties.DetermineGateSide(transform.position) != this.side && flickProperties.Contained(transform.position)) {
        this.flicked = false;
        Camera.main.SendMessage("CoinDeselected");
      } else if(rigidbody2D.velocity.magnitude <= stoppedVelocity && flickProperties.timer >= delayTimer) {
        if(GameOver.gameStarted) {
          Camera.main.SendMessage("EndGame", false);
        } else {
          GameOver.gameStarted = true;
        }
        this.flicked = false;
        Camera.main.SendMessage("CoinDeselected");
      }
    }
  }

  public void Flick(FlickProperties props) {
    this.flickProperties = props;
    Vector2 start = new Vector2(props.start.x, Screen.height - props.start.y);
    Vector2 end = new Vector2(props.end.x, Screen.height - props.end.y);
    float power = MAX_FORCE * (Mathf.Min((start-end).magnitude, MAX_FLICK_RANGE)) / MAX_FLICK_RANGE;
    
    Vector2 force = (start-end).normalized * power;
    force.x *= -1.0f;
    
    rigidbody2D.AddForce(force);
    
    this.side = props.DetermineGateSide(transform.position);
    flicked = true;
  }

  public void Select() {
    GetComponent<SpriteRenderer>().sprite = Resources.Load("yellow_x", typeof(Sprite)) as Sprite;
  }
  
  public void Deselect() {
    GetComponent<SpriteRenderer>().sprite = Resources.Load("blue_x", typeof(Sprite)) as Sprite;
  }

  void OnMouseDown() {
    if(!GameOver.over) {
      Camera.main.SendMessage("SelectedNewCoin", gameObject);
    }
  }
  
  void OnCollisionEnter2D(Collision2D collision) {
    if(GameOver.gameStarted && !GameOver.over) {
      Camera.main.SendMessage("EndGame", false);
      this.flicked = false;
      Camera.main.SendMessage("CoinDeselected");
    }
  }
  
  void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Goal" && !GameOver.over && !flicked) {
      Camera.main.SendMessage("EndGame", true);
      this.flicked = false;
      Camera.main.SendMessage("CoinDeselected");
    }
  }

}

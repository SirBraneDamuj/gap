using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

  public bool flicked = false;
  public float stoppedVelocity = 0.1f;
  public float delayTimer = 0.3f;
  public CoinManager cm;
  
  //range in pixels that you can drag your finger. a flick at this range = biggest power
  public float maxFlickRange=200.0f;
  //highest end of the force spectrum
  public float maxForce = 1000.0f;
  
  private FlickProperties flickProperties;
  private float side;
  
  void Start() {
    maxFlickRange = Screen.height * 0.30f;
  }
  
  void Update() {
    if(Mathf.Abs(rigidbody2D.velocity.magnitude) <= stoppedVelocity) {
      rigidbody2D.velocity = Vector3.zero;
    }
    if(flicked) {
      flickProperties.timer += Time.deltaTime;
      flickProperties.DetermineClear(transform.position, this.side);
      if(rigidbody2D.velocity.magnitude <= stoppedVelocity && flickProperties.timer >= delayTimer && !GameManager.Over()) {
        if(GameManager.Pregame()) {
          GameManager.StartGame();
        } else if(!flickProperties.cleared) {
          GameOver(false);
        }
        this.flicked = false;
        cm.SendMessage("CoinDeselected");
      }
    } else if(Input.touchCount > 0) {
      Touch t = Input.GetTouch(0);
      
      if(t.phase == TouchPhase.Began) {
        Vector2 world = Camera.main.ScreenToWorldPoint(t.position);
        if(!GameManager.Over() && collider2D.OverlapPoint(world)) {
          cm.CoinClicked(gameObject);
        }
      }
    }
  }

  public void Flick(FlickProperties props) {
    this.flickProperties = props;
    Vector2 start = new Vector2(props.start.x, Screen.height - props.start.y);
    Vector2 end = new Vector2(props.end.x, Screen.height - props.end.y);
    float power = maxForce * (Mathf.Min((start-end).magnitude, maxFlickRange)) / maxFlickRange;
    
    Vector2 force = (start-end).normalized * power;
    force.x *= -1.0f;
    
    rigidbody2D.AddForce(force);
    
    this.side = props.DetermineGateSide(transform.position);
    flicked = true;
  }

  public void Select() {
    if(!GameManager.Over()) {
      GetComponent<SpriteRenderer>().sprite = Resources.Load("yellow_x", typeof(Sprite)) as Sprite;
    }
  }
  
  public void Deselect() {
    if(!GameManager.Over()) {
      GetComponent<SpriteRenderer>().sprite = Resources.Load("blue_x", typeof(Sprite)) as Sprite;
    }
  }
  
  public void Dead() {
    GetComponent<SpriteRenderer>().sprite = Resources.Load("red_x", typeof(Sprite)) as Sprite;
  }
  
  void OnCollisionEnter2D(Collision2D collision) {
    if(GameManager.InProgress()) {
      GameOver(false);
      collision.collider.SendMessage("Dead", SendMessageOptions.DontRequireReceiver);
    }
  }
  
  void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Goal" && !GameManager.Over()) {
      GameOver(flickProperties.cleared);
    }
  }
  
  void GameOver(bool victory) {
    GameManager.EndGame(victory);
    this.flicked = false;
    if(victory) {
      Camera.main.SendMessage("CoinDeselected");
    } else {
      Dead();
    }
  }

}

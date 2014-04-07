using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

  public bool flicked = false;
  public int gatesPassed = 0;
  public float stoppedVelocity = 0.1f;
  
  void Update() {
    if(Mathf.Abs(rigidbody2D.velocity.magnitude) <= stoppedVelocity) {
      rigidbody2D.velocity = Vector3.zero;
    }
    if(flicked && gatesPassed < 2 && Mathf.Abs(rigidbody2D.velocity.magnitude) <= stoppedVelocity) {
      if(!GameOver.gameStarted) {
        GameOver.gameStarted = true;
      } else {
        Camera.main.SendMessage("EndGame", false);
      }
      flicked = false;
      gatesPassed = 0;
    }
  }

  public void Select() {
    GetComponent<SpriteRenderer>().sprite = Resources.Load("yellow_x", typeof(Sprite)) as Sprite;
  }
  
  public void Deselect() {
    GetComponent<SpriteRenderer>().sprite = Resources.Load("red_x", typeof(Sprite)) as Sprite;
  }

  void OnMouseDown() {
    Camera.main.SendMessage("SelectedNewCoin", gameObject);
  }
  
  void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Gate") {
      gatesPassed += 1;
      if(gatesPassed >= 2) {
        flicked = false;
        Camera.main.SendMessage("CoinDeselected");
        gatesPassed = 0;
      }
    }
  }

}

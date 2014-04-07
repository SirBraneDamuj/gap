using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

  public void Select() {
    GetComponent<SpriteRenderer>().sprite = Resources.Load("yellow_x", typeof(Sprite)) as Sprite;
  }
  
  public void Deselect() {
    GetComponent<SpriteRenderer>().sprite = Resources.Load("red_x", typeof(Sprite)) as Sprite;
  }

  void OnMouseDown() {
    Camera.main.SendMessage("SelectedNewCoin", gameObject);
  }

}

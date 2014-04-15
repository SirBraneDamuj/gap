using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {
  
  public Coin[] coins;
  public Coin selected = null;
  public Coin firstCoin;
  public Coin[] nonSelected {
    get {
      Coin[] cs = new Coin[2];
      int i=0;
      foreach(Coin c in coins) {
        if(c != selected) {
          cs[i] = c;
          i++;
        }
      }
      return cs;
    }
  }
  public int numFlicks = 0;
  private MouseFlick flicker;
  
  void Start() {
    flicker = GetComponent<MouseFlick>();
  }
  
  void CoinClicked(GameObject coin) {
    if(GameManager.Pregame() && coin != firstCoin.gameObject) return;
    if(selected == null) {
      selected = coin.GetComponent<Coin>();
      selected.Select();
      flicker.StartDrag(coin);
    }
  }
  
  public void FlickSelected(Vector2 direction) {
    FlickProperties props = new FlickProperties(selected.transform.position, direction, nonSelected);
    if(!props.ValidFlick()) {
      selected.Flick(new FlickProperties(selected.transform.position, direction, nonSelected));
      if(!GameManager.Pregame()) {
        numFlicks++;
      }      
    } else {
      CoinDeselected();
    }
  }
  
  public void CoinDeselected() {
    if(selected != null) {
      selected.Deselect();
      selected = null;
    }
  }
}

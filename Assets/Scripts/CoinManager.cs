using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {
  
  public Coin[] coins;
  public Coin selected = null;
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
  
  public void FlickSelected(Vector2 direction) {
    selected.Flick(new FlickProperties(selected.transform.position, direction, nonSelected));
  }
  
  public void Select(GameObject newTarget) {
    if(selected != null) {
      selected.Deselect();
    }
    selected = newTarget.GetComponent<Coin>();
    selected.Select();
  }
  
  public void CoinDeselected() {
    if(selected != null) {
      selected.Deselect();
      selected = null;
    }
  }
}

using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {
  
  public Coin[] coins;
  public Coin firstCoin;
  public int numFlicks = 0;
  
  public Coin[] NonSelected(Coin s) {
    Coin[] cs = new Coin[2];
    int i=0;
    foreach(Coin c in coins) {
      if(c != s) {
        cs[i] = c;
        i++;
      }
    }
    return cs;
  }
  
  public Coin TouchedCoin(Vector2 t) {
    Debug.Log(t);
    Coin retval = null;
    Vector2 world = Camera.main.ScreenToWorldPoint(t);
    for(int i=0; i<coins.Length; i++) {
      Coin c = coins[i];
      if(!GameManager.Over() && c.collider2D.OverlapPoint(world)) {
        retval = c;
      }
    }
    return retval;
  }
}

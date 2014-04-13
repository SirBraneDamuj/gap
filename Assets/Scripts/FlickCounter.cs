using UnityEngine;
using System.Collections;

public class FlickCounter : MonoBehaviour {

  public CoinManager cm;

	void OnGUI() {
    GUI.Box(new Rect(0,0,75,25), "Flicks: " + cm.numFlicks);
  }
}

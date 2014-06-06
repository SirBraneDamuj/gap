using UnityEngine;
using System.Collections;

public class TouchFlick : MonoBehaviour {
  
  public CoinManager coinManager;
  public float flickDuration;
  
  private float flickTimer = 0.0f;
  private bool startedFlick = false;

	// Use this for initialization
	void Start () {
    coinManager = GetComponent<CoinManager>();
	}
	
	// Update is called once per frame
	void Update () {
    if(startedFlick) {
      flickTimer += Time.deltaTime;
      bool touchEnded = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
      if(flickTimer >= flickDuration || touchEnded) {
        coinManager.FlickSelected(Input.GetTouch(0).position);
        startedFlick = false;
      }
    }
	}
  
  public void StartDrag(GameObject coin) {
    if(!GameManager.Over()) {
      startedFlick = true;
      flickTimer = 0.0f;
    }
  }
}

using UnityEngine;
using System.Collections;

public class MouseFlick : MonoBehaviour {

  public CoinManager coinManager;
  public float flickDuration;
  
  private float flickTimer = 0.0f;
  private bool startedFlick = false;
  
  void Start() {
    coinManager = GetComponent<CoinManager>();
  }
	
	// Update is called once per frame
	void Update () {
    if(startedFlick) {
      flickTimer += Time.deltaTime;
      if(flickTimer >= flickDuration || Input.GetMouseButtonUp(0)) {
        coinManager.FlickSelected(Input.mousePosition);
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
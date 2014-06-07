using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

  public CoinManager coinManager;
  public float flickDuration;
  public int numFlicks = 0;
  public InputType input;
  
  private float flickTimer = 0.0f;
  private bool startedDrag = false;
  private Coin selected;

	// Use this for initialization
	void Start () {
    coinManager = GetComponent<CoinManager>();
    input = new TouchInputType();
	}
	
	// Update is called once per frame
	void Update () {
    if(startedDrag) {
      flickTimer += Time.deltaTime;
      if(flickTimer >= flickDuration || input.InputEnded()) {
        Flick(input.CurrentInput());
      }
    } else {
      if(input.InputStarted()) {
        Coin c = coinManager.TouchedCoin(input.CurrentInput());
        if(c != null) {
          SelectCoin(c);
        }
      }
    }
	}
  
  void SelectCoin(Coin c) {
    c.Select();
    if(GameManager.Pregame() && c.gameObject.name != coinManager.firstCoin.gameObject.name) return;
    c.Select();
    selected = c;
    startedDrag = true;
    flickTimer = 0.0f;
  }
  
  void DeselectCoin() {
    selected.Deselect();
    selected = null;
  }
  
  void Flick(Vector2 end) {
    startedDrag = false;
    FlickProperties props = new FlickProperties(selected.transform.position, end, coinManager.NonSelected(selected));
    if(props.ValidFlick()) {
      selected.Flick(props);
      if(!GameManager.Pregame()) {
        numFlicks++;
      }      
    } else {
      DeselectCoin();
    }
  }
}

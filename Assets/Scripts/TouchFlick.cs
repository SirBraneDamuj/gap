using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if(Input.touchCount > 0) {
      Touch t = Input.GetTouch(0);
      Debug.Log(t);
    }
	}
}

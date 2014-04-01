using UnityEngine;
using System;
using System.Collections;

public class TrackingCamera : MonoBehaviour {

  public Transform[] targets;
  
  public float trackingSpeed;
  
  public float zoomingSpeed;
  public float minSize;
  public float maxSize;
  
  public float updateSpeed;
  
  [HideInInspector]
  public Vector3 targetPosition;
  [HideInInspector]
  public float targetSize;
  
  private bool stop = false;
  

	// Use this for initialization
	void Start () {
    StartCoroutine(UpdateTargetPosition());
    StartCoroutine(UpdateSize());
	}
	
	// Update is called once per frame
	void Update () {
    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * trackingSpeed);
    camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, Time.deltaTime * zoomingSpeed);
	}
  
  IEnumerator UpdateTargetPosition() {
    while(!stop) {
      Vector3 sum = Vector3.zero;
      foreach(Transform t in targets) {
        sum.x += t.position.x;
        sum.y += t.position.y;
      }
      sum /= targets.Length;
      sum.z = -10;
      targetPosition = sum;
      yield return new WaitForSeconds(updateSpeed);
    }
  }
  
  IEnumerator UpdateSize() {
    while(!stop) {
      float[] distances = new float[targets.Length];
      for(int i=0; i<distances.Length; i++) {
        distances[i] = (targets[i].position - targetPosition).magnitude;
      }
      Array.Sort(distances);
      Array.Reverse(distances);
      targetSize = Mathf.Lerp(minSize, maxSize, 2 * (distances[0] - distances[1]) / distances[1]);
      yield return new WaitForSeconds(updateSpeed);
    }
  }
}

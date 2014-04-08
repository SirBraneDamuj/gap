using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

  public AudioClip gameOver;
  
  public static bool gameStarted = false;
  public static bool over = false;
  
  public string message = "RAGGLE FRAGGLE!";

	IEnumerator EndGame(bool won) {
    if(won) {
      message = "YOU WIN!!!";
    } else if(gameStarted) {
      message = "YOU LOSE!!!";
    }
    over = true;
    audio.Play();
    yield return new WaitForSeconds(audio.clip.length + 1.0f);
    gameStarted = false;
    over = false;
    Application.LoadLevel(0);
  }
  
  void OnGUI() {
    if(over) { GUI.Box(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 25, 150, 50), message); }
  }
}

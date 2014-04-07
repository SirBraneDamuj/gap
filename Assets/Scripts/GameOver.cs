using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

  public AudioClip gameOver;
  
  public static bool gameStarted = false;

	IEnumerator EndGame(bool won) {
    if(won) {
      //yay
    } else if(gameStarted) {
      audio.clip = gameOver;
      audio.Play();
      yield return new WaitForSeconds(audio.clip.length + 1.0f);
      gameStarted = false;
      Application.LoadLevel(0);
    }
  }
}

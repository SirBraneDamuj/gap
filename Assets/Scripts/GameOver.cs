using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

  public AudioClip gameOver;
  
  public static bool gameStarted = false;
  public static bool over = false;
  
  public GameObject gameOverSprite;
  public GameObject winSprite;
  
  public string message = "RAGGLE FRAGGLE!";

	IEnumerator EndGame(bool won) {
    if(won) {
      winSprite.GetComponent<SpriteRenderer>().enabled = true;
    } else {
      gameOverSprite.GetComponent<SpriteRenderer>().enabled = true;
    }
    over = true;
    audio.Play();
    yield return new WaitForSeconds(audio.clip.length + 1.0f);
    gameStarted = false;
    over = false;
    Application.LoadLevel(0);
  }
}

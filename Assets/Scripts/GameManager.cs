using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

  public enum GameState {
    PREGAME,
    STARTED,
    LOSS,
    VICTORY
  };
  
  public static GameState gameState;
  private static GameManager instance;
  
  public static bool Pregame() {
    return gameState == GameState.PREGAME;
  }
  
  public static bool InProgress() {
    return gameState == GameState.STARTED;
  }
  
  public static bool Over() {
    return gameState == GameState.LOSS || gameState == GameState.VICTORY;
  }
  
  public static void StartGame() {
    gameState = GameState.STARTED;
  }
  
  public static void EndGame(bool won) {
    if(won) {
      instance.winSprite.GetComponent<SpriteRenderer>().enabled = true;
      gameState = GameState.VICTORY;
    } else {
      instance.gameOverSprite.GetComponent<SpriteRenderer>().enabled = true;
      gameState = GameState.LOSS;
    }
    instance.SendMessage("EndGameCoroutine");
  }
  
  public GameObject gameOverSprite;
  public GameObject winSprite;
  
  void Start() {
    gameState = GameState.PREGAME;
    instance = gameObject.GetComponent<GameManager>();
  }
  
  void Update() {
    if(Input.GetKey(KeyCode.Escape)) {
      Application.Quit();
    }
  }
  
  void EndGameCoroutine() {
    StartCoroutine(EndGameImpl());
  }

	IEnumerator EndGameImpl() {
    audio.Play();
    yield return new WaitForSeconds(audio.clip.length);
    Application.LoadLevel(0);
  }
}

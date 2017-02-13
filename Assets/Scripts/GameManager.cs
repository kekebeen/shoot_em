using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject PlayerGO;
	public GameObject PlayButton;
	public GameObject EnemySpawner;
	public GameObject GameOverGO;
	public GameObject ScoreTextGO;
	public GameObject BackgroundMusicGO;
	public GameObject LevelMusicGO;
	public GameObject ShootButtonGO;
	public enum GameManagerState
	{
		Opening,
		Gameplay,
		GameOver,
	}

	GameManagerState GMState;
	// Use this for initialization
	void Start () {
		GMState = GameManagerState.Opening;
	}
	
	// Update is called once per frame
	void UpdateGameManagerState () {

		switch (GMState) {
		case GameManagerState.Opening:
			PlayButton.SetActive (true);
			GameOverGO.SetActive (false);
			EnemySpawner.GetComponent<EnemySpawner> ().StopEnemySpawner ();
			break;
		case GameManagerState.Gameplay:
			ScoreTextGO.GetComponent<GameScore> ().Score = 0;
			PlayButton.SetActive (false);
			PlayerGO.GetComponent<PlayerMovement> ().Init ();
			EnemySpawner.GetComponent<EnemySpawner> ().InitEnemySpawner ();
			GameOverGO.SetActive (false);
			ShootButtonGO.SetActive (true);
			//Play
			BackgroundMusicGO.GetComponent<AudioSource> ().Stop ();
			LevelMusicGO.GetComponent<AudioSource> ().Play ();
			break;
		case GameManagerState.GameOver:
			EnemySpawner.GetComponent<EnemySpawner> ().StopEnemySpawner ();
			GameOverGO.SetActive (true);
			LevelMusicGO.GetComponent<AudioSource> ().Stop ();
			BackgroundMusicGO.GetComponent<AudioSource> ().Play ();
			EnemySpawner.GetComponent<EnemySpawner> ().DestroAllMobs ();
			ShootButtonGO.SetActive (false);
			Invoke ("ChangeToOpeningState", 1f);
			break;
		}
	}

	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState ();
	}

	public void StartGame(){
		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState ();
	}

	public void ChangeToOpeningState()
	{
		SetGameManagerState (GameManagerState.Opening);
	}

}

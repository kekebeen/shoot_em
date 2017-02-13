using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

	Text ScoreText;
	int score;

	public int Score
	{
		get {
			return score;
		}

		set{
			this.score = value;
			UpdateScoreTextUI ();
		}
	}
	// Use this for initialization
	void Start () {
		ScoreText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateScoreTextUI(){
		string scoreString = string.Format ("{0:00000}", score);
		ScoreText.text = scoreString;
	}
}

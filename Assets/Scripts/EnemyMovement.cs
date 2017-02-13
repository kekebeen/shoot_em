using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public GameObject ExplosionGO;
	GameObject ScoreTextGO;
	public float speed;
	// Use this for initialization
	void Start () {
		speed = 1f;

		ScoreTextGO = GameObject.FindGameObjectWithTag ("ScoreTextTag");
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position = transform.position;

		position = new Vector2 (position.x,position.y - speed * Time.deltaTime);

		transform.position = position;

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

		if (transform.position.y < min.y) {
			Destroy (gameObject);
		}

	}
	void OnTriggerEnter2D(Collider2D col){
		if( (col.gameObject.tag == "PlayerGO") || (col.gameObject.tag == "PlayerBulletGO")){
			Explode ();
			Destroy (gameObject);
			ScoreTextGO.GetComponent<GameScore> ().Score += 100;
		}
	}

	void Explode(){
		GameObject enemyShipExplosion = (GameObject)Instantiate (ExplosionGO);

		enemyShipExplosion.transform.position = transform.position;
	}
}

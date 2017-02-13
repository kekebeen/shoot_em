using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float speed = 16f;
	public GameObject PlayerBulletGO;
	public GameObject PlayerGO;
	public GameObject BulletPosition01;
	public GameObject ExplosionGO;
	public GameObject GameManagerGO;

	public Text LivesUIText;
	const int MaxLives = 3;
	int lives;

	float accelStartY;
	// Use this for initialization
	void Start () {
		lives = MaxLives;
		LivesUIText.text = lives.ToString ();
		gameObject.SetActive (true);

		accelStartY = Input.acceleration.y;
	}

	public void Init()
	{
		lives = MaxLives;
		LivesUIText.text = lives.ToString ();
		gameObject.SetActive (true);
		//(GameObject)playerGO = (GameObject)Instantiate (gameObject);
	}
	// Update is called once per frame
	void Update () {

		//disabling accelerometer for testing keyboard controls
		//float x = Input.acceleration.x;
		//float y = Input.acceleration.y - accelStartY;

		//keyboard controls temp
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		Vector2 direction = new Vector2 (x, y);

		if (direction.sqrMagnitude > 1)
			direction.Normalize ();

		Move (direction);
	}

	void Move(Vector2 direction){
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		max.x = max.x - 0.225f;
		min.x = min.x + 0.225f;

		max.y = max.y - 0.285f;
		min.y = min.y + 0.285f;

		Vector2 position = transform.position;

		position += direction * speed * Time.deltaTime;

		position.x = Mathf.Clamp (position.x, min.x, max.x);
		position.y = Mathf.Clamp (position.y, min.y, max.y);

		transform.position = position; 
	}

	void OnTriggerEnter2D(Collider2D col){
		if( (col.gameObject.tag == "EnemyShipGO") || (col.gameObject.tag == "EnemyBulletGO")){
			Explode ();
			lives--;
			LivesUIText.text = lives.ToString ();

			if (lives == 0) {
				//Game
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
				//Destroy (gameObject);
				gameObject.SetActive(false);
			}
		}
	}

	void Explode(){
		GameObject playerExplosion = (GameObject)Instantiate (ExplosionGO);

		playerExplosion.transform.position = transform.position;
	}

	public void Shoot(){
		gameObject.GetComponent<AudioSource> ().Play ();
		GameObject Bullet01 = (GameObject)Instantiate (PlayerBulletGO);
		Bullet01.transform.position = BulletPosition01.transform.position;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject EnemyGO;
	public float spawnRate = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnEnemy (){
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		GameObject enemy = (GameObject)Instantiate (EnemyGO);

		enemy.transform.position = new Vector2 (Random.Range(min.x,max.x), max.y);

		SpawnNextEnemy ();
	}

	void SpawnNextEnemy(){
		float spawnInSeconds;

		if (spawnRate > 1f) {
			spawnInSeconds = Random.Range (1f, spawnRate);
		} else {
			spawnInSeconds = 1f;
		}

		Invoke ("SpawnEnemy", spawnInSeconds);
	}

	void IncreaseSpawnRate(){
		if (spawnRate > 1f) {
			spawnRate--;
		}

		if (spawnRate == 1f) {
			CancelInvoke ("IncreaseSpawnRate");
		}
	}

	public void StopEnemySpawner()
	{
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("IncreaseSpawnRate");
	}

	public void InitEnemySpawner()
	{
		spawnRate = 5f;
		Invoke ("SpawnEnemy",	spawnRate);

		InvokeRepeating("IncreaseSpawnRate",0f,30f);
	}

	public void DestroAllMobs(){
		GameObject[] mobs = GameObject.FindGameObjectsWithTag ("EnemyShipGO");
		foreach (GameObject mob in mobs) {
			Destroy (mob);
		}
	}
}

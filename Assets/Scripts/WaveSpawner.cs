using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;


/**
 * Manages spawning enemy waves for a level.
 */
public class WaveSpawner : MonoBehaviour {

	[Header("Enemy Settings")]

	[Tooltip("The enemy type that this spawner should create.")]
	public Transform enemyPrefab;

	[Tooltip("Where the enemy spawns from.")]
	public Transform spawnPoint;

	[Tooltip("The delay between spawning individual enemies.")]
	public float spawnDelay = 0.5f;


	[Header("Wave Settings")]

	[Tooltip("The amount of time, in seconds, between waves.")]
	public float waveTimePeriod = 30f;

	[Tooltip("The time remaining until the next wave (with 2 seconds delay before the first wave).")]
	[ReadOnly]
	[SerializeField]
	protected float countdown = 2f;


	[Header("UI Settings")]
	public Text countdownText;


	/**
	 * The wave number.
	 */
	protected int waveNumber = 0;


	/**
	 * Runs wavespawner logic each frame.
	 */
	public void Update () {

		countdown -= Time.deltaTime;

		countdownText.text = (Mathf.Floor (countdown + 1) == 0 ? waveTimePeriod : Mathf.Floor (countdown + 1)).ToString ();

		// Spawn a new wave of enemies if we're at the end of our timer
		if (countdown <= 0f) {
			StartCoroutine (SpawnWave ());
			countdown = waveTimePeriod;
		}

	}


	/**
	 * Spawns the next wave of enemies.
	 */
	public IEnumerator SpawnWave () {

		waveNumber++;

		for (int i = 0; i < waveNumber; i++) {
			SpawnEnemy (enemyPrefab);
			yield return new WaitForSeconds (spawnDelay);
		}

	}


	/**
	 * Spawns an enemy from the spawner.
	 */
	public void SpawnEnemy (Transform enemy) {

		Instantiate (enemy, spawnPoint.position, spawnPoint.rotation);

	}

}

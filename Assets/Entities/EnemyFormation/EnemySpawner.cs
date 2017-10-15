using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	private bool movingRight = true;
	public float speed = 5f;
	private float xmin;
	private float xmax;
	public float spawnDelay = 1f;

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));
		xmin = leftEdge.x;
		xmax = rightEdge.x;

		SpawnUntilFull();

	}


	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
		} else {
			transform.position += new Vector3 (-speed*Time.deltaTime,0,0);
		}

		//restrict the formation to the gamespace

		if ((transform.position.x + 0.5f * width) > xmax ) {
			movingRight = false;
		} else if((transform.position.x - 0.5f * width) < xmin) {
			movingRight = true;
		}

		if (AllMembersDead ()) {
			Debug.Log ("Empty formation");
			SpawnUntilFull();
		}
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width,height,0));
	}

	Transform NextFreePosition() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			} 
		}
		return null;
	}

	bool AllMembersDead() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			} 
		}
		return true;
	}

	void SpawnEnemies() {
		foreach(Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
}

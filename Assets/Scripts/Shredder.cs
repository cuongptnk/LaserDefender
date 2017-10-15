using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {
	void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3 (32.8f, 2.4f, 1f));
	}

	void OnTriggerEnter2D(Collider2D col) {
		Destroy (col.gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float health = 150f;
	public float projectileSpeed = 10f;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	private ScoreKeeper scoreKeeper;
	public AudioClip fireSound;
	public AudioClip deathSound;


	void Start() {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void Update() {
		float probability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0,-1,0);
		GameObject missile = Instantiate (projectile, startPosition ,Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,-projectileSpeed);
		AudioSource.PlayClipAtPoint (fireSound,transform.position);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Projectile missile = col.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			if (health <= 0) {
				scoreKeeper.ScoreUpdate (scoreValue);
				AudioSource.PlayClipAtPoint (deathSound,transform.position);
				Destroy (gameObject);
			}
			missile.Hit ();
		}
	}

}

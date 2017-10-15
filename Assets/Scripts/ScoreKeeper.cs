using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	public static int score = 0;
	private Text scoreText;

	void Start() {
		scoreText = GetComponent<Text> ();
		Reset ();
	}

	public void ScoreUpdate(int points) {
		score += points;
		scoreText.text = score.ToString();
	}

	public static void Reset() {
		score = 0;
		//scoreText.text = score.ToString();
	}

}

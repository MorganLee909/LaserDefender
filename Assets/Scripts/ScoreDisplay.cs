using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
    private ScoreKeeper scoreKeeper;

	void Start () {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		Text myText = GetComponent<Text>();
		myText.text = scoreKeeper.getScore().ToString();
		print (scoreKeeper.getScore().ToString ());
	}
}

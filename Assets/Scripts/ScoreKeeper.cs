using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
	private static int score;
	private Text myText;
	
	void Start(){
        score = 0;
		myText = GetComponent<Text>();
	}
	
    /// <summary>
    /// Adds 'points' to the total score and updates the text display
    /// </summary>
    /// <param name="points"></param>
	public void Score(int points){
		score += points;
		myText.text = score.ToString ();
	}
	
    /// <summary>
    /// Resets the score to 0
    /// </summary>
	public static void Reset(){
		score = 0;
	}

    public int getScore() {
        return score;
    }
}

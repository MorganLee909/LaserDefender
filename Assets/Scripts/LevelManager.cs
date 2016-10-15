using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    /// <summary>
    /// loads the appropriately called level
    /// </summary>
    /// <param name="name"></param>
	public void LoadLevel(string name) {
        SceneManager.LoadScene(name);
	}
	
    /// <summary>
    /// Ends the game
    /// </summary>
	public void Quit() {
		Application.Quit ();
	}
}

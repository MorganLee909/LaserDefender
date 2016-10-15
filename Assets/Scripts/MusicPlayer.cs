using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	public AudioClip startClip, gameClip, endClip;

    private static MusicPlayer instance = null;
    private AudioSource music;
	
	void Start () {
		if(instance != null && instance != this) {
			Destroy(gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play ();
		}
		ScoreKeeper.Reset ();
	}
	
    /// <summary>
    /// Plays music based on the level that was loaded
    /// </summary>
    /// <param name="level"></param>
	private void OnLevelWasLoaded(int level) {
		Debug.Log("Musicplayer: loaded level " + level);
		music.Stop ();
		if (level == 0) {
			music.clip = startClip;
		} else if(level == 1) {
			music.clip = gameClip;
		} else if(level == 2) {
			music.clip = endClip;
		}
		music.loop = true;
		music.Play ();
	}
}

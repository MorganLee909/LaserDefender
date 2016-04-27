﻿using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip, gameClip, endClip;
	
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
	
	void OnLevelWasLoaded(int level) {
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
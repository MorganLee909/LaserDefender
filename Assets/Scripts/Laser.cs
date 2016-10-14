using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    public bool playerLaser;

	void OnTriggerEnter2D(Collider2D collider) {

        if (playerLaser) {
            if (collider.tag == "Enemy" || collider.tag == "Shredder") {
                Destroy(gameObject);
            }
        }
        if (!playerLaser) {
            if(collider.tag == "Player" || collider.tag == "Shredder") {
                Destroy(gameObject);
            }
        }
	}
}

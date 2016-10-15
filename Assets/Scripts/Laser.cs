using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    public bool playerLaser;

    private float speed;

    void Start() {
        if (playerLaser) {
            speed = 10f;
        }else if (!playerLaser) {
            speed = -10f;
        }
    }

    void Update() {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    /// <summary>
    /// Destroys the laser when it collides with the 
    /// appropriate target
    /// </summary>
    /// <param name="collider"></param>
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

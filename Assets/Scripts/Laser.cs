using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;
	public float padding = 0.5f;
	public GameObject projectile;
	public static float laserSpeed = 10f;
	public float fireRate = 0.2f;
	
	private float health = 500f;
	
	float xMin;
	float xMax;
	
	void Start() {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftLimit.x + padding;
		xMax = rightLimit.x - padding;
	}

	void Update() {
		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
		if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		if(Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, fireRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	}
	
	void Fire() {
		Vector3 laserPosition = transform.position + new Vector3(0f, .75f, 0f);
		GameObject playerLaser = Instantiate(projectile,laserPosition, Quaternion.identity) as GameObject;
		playerLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, laserSpeed);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		float enemyDamage = 75f;
		health -= enemyDamage;
		if(health <=0f){
			Die();		
		}
	}
	
	void Die() {
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Lose");
	}
}
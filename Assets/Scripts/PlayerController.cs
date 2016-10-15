using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject projectile;
    public float health;

    private float fireRate;
    private float speed;
	private float xMin, xMax;
    private float damage;
	
	void Start() {
        fireRate = 0.2f;
        speed = 10f;
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftLimit.x + 0.5f;  //adds padding
		xMax = rightLimit.x - 0.5f;  //adds padding
        damage = 125f;
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
	
    /// <summary>
    /// Fires a laser and gives it motion
    /// </summary>
	void Fire() {
		Vector3 laserPosition = transform.position + new Vector3(0f, .75f, 0f);
		GameObject playerLaser = Instantiate(projectile,laserPosition, Quaternion.identity) as GameObject;
	}
	
    /// <summary>
    /// When hit by an enemy laser, deals damage and calls Die()
    /// if health is below 0.
    /// </summary>
    /// <param name="collider"></param>
	void OnTriggerEnter2D(Collider2D collider){
		float enemyDamage = 75f;
		health -= enemyDamage;
		if(health <=0f){
			Die();		
		}
	}
	
    /// <summary>
    /// Goes to end screen when player is killed
    /// </summary>
	void Die() {
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Lose");
	}

    public float getDamage() {
        return damage;
    }
}
using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public float health = 250f;
	public GameObject projectile;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 125;
	public AudioClip explosion;
	
	private float playerDamage = 100f;
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Enemy"){
            health -= playerDamage;
        }
		if(health <= 0){
			Destroy(gameObject);
			scoreKeeper.Score(scoreValue);
			AudioSource.PlayClipAtPoint(explosion, transform.position);
		}
	}
	
	void Update() {
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire();
		}
	}
	
	void Fire() {
		Vector3 laserPosition = transform.position + new Vector3(0f, -0.75f, 0f);
		GameObject enemyLaser =	Instantiate(projectile, laserPosition, Quaternion.identity) as GameObject;
		enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -PlayerController.laserSpeed);
	}
}

using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
    public float health;
	public GameObject projectile;
    public float shotsPerSecond;
    public int scoreValue;
	public AudioClip explosion;

    private PlayerController player;
	private ScoreKeeper scoreKeeper;
	
	void Start(){
        player = GameObject.Find("Player").GetComponent<PlayerController>();
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update() {
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire();
		}
	}

    /// <summary>
    /// Detects a collision with th players laser.  If health below 0
    /// then ship is destroyed and score updated
    /// </summary>
    /// <param name="collider"></param>
	void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "PlayerLaser(Clone)") {
            health -= player.getDamage();
        }
        if (health <= 0) {
            Destroy(gameObject);
            scoreKeeper.Score(scoreValue);
            AudioSource.PlayClipAtPoint(explosion, transform.position);
        }
    }

    /// <summary>
    /// Fires a laser and gives it motion
    /// </summary>
    void Fire() {
		Vector3 laserPosition = transform.position + new Vector3(0f, -0.75f, 0f);
		GameObject enemyLaser =	Instantiate(projectile, laserPosition, Quaternion.identity) as GameObject;
	}
}

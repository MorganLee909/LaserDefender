using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float speed;
	public float spawnDelay;

    private float width;
    private float height;
    private bool movingRight;
	private float xMax;
	private float xMin;

	void Start () {
        movingRight = true;
        width = 10f;
        height = 5f;
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 cameraLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,1, distanceToCamera));
		Vector3 cameraRight = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xMax = cameraRight.x;
		xMin = cameraLeft.x;
		SpawnUntilFull();
	}
	
	void Update() {
		if(movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float formationRight = transform.position.x + (0.5f * width);
		float formationLeft = transform.position.x - (0.5f * width);
		if(formationRight > xMax) {
			movingRight = false;
		}else if(formationLeft < xMin){
			movingRight = true;
		}
		if(AllDead()){
			SpawnUntilFull();
		}		
	}

    /// <summary>
    /// Creates new enemies after all are killed
    /// </summary>
	void SpawnUntilFull() {
        Transform freePosition = NextFreePosition();
        if (freePosition) {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition()) {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    /// <summary>
    /// Creates a wire cube gizmo for the enemies
    /// </summary>
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    /// <summary>
    /// Finds a free position in the gizmo based on whether
    /// it has any children
    /// </summary>
    /// <returns></returns>
    private Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0f){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
    /// <summary>
    /// Checks to see whether all of the enemies are currently
    /// dead or not
    /// </summary>
    /// <returns></returns>
	private bool AllDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0f){
				return false;
			}
		}
		return true;
	}
}
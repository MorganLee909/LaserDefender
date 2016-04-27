using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 1;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float xMax;
	private float xMin;

	void Start () {	
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 cameraLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,1, distanceToCamera));
		Vector3 cameraRight = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xMax = cameraRight.x;
		xMin = cameraLeft.x;
		SpawnUntilFull();
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}
	
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height));
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
	
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount == 0f){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount > 0f){
				return false;
			}
		}
		return true;
	}
}
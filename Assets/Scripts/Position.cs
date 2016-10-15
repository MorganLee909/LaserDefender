using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

    /// <summary>
    /// Creates the sphere gizmos for the individual enemies
    /// </summary>
	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, 1.0f);
	}
}
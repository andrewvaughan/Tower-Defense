using UnityEngine;
using System.Collections.Generic;


/**
 * Maintains data about the environment for a level.
 */
public class Environment : MonoBehaviour {

	/**
	 * A linked-list of waypoints in the environment.
	 */
	public static LinkedList<Transform> waypoints;


	/**
	 * Loads any children into the waypoints List.
	 */
	public void Awake() {
		
		waypoints = new LinkedList<Transform>();

		for (int i = 0; i < transform.childCount; i++) {
			waypoints.AddLast (transform.GetChild (i));
		}

	}

}

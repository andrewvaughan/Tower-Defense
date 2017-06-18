using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	/**
	 * The speed of the enemy's movement.
	 */
	public float speed = 10f;

	/**
	 * The waypoint LinkedListNode this enemy is targeting.
	 */
	protected LinkedListNode<Transform> target;


	/**
	 * Targets the first waypoint in the Environment.
	 */
	public void Start() {
		
		target = Environment.waypoints.First;

	}


	/**
	 * Moves the enemy toward the given waypoint each frame.
	 */
	public void Update() {

		// Calculate the direction based on enemy position and target waypoint
		Vector3 direction = target.Value.position - transform.position;

		// Normalize the direction to allow our speed to be controlled by the public attribute
		transform.Translate (direction.normalized * speed * Time.deltaTime, Space.World);

		// Switch targets if we reach the waypoint
		if (Vector3.Distance (transform.position, target.Value.position) <= (speed / 50)) {

			// Move to the next waypoint, unless we've reached the end
			if (!NextWaypoint ()) {
				Destroy(gameObject);
			}

		}

	}


	/**
	 * Targets the next waypoint for the Enemy.
	 * 
	 * <returns>True if the waypoint was retargeted, or False if the enemy has reached the end</returns>
	 */
	protected bool NextWaypoint() {

		// If there's no next waypoint, we've reached the end
		if (target.Next == null) {
			return false;
		}

		// Set our position to the current target, pointing at the next target
		transform.SetPositionAndRotation (target.Value.position, Quaternion.LookRotation (target.Next.Value.position));

		// Set our target to the next waypoint
		target = target.Next;

		return true;

	}

}

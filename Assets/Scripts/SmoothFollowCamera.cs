using UnityEngine;
using System.Collections;

public class SmoothFollowCamera : MonoBehaviour {
	public Transform target;
	public float speed;
	
	private Vector3 offset;
	
	// Use this for initialization
	void Start ()
	{
		offset = target.position - transform.position;
	}

	void LateUpdate ()
	{
		transform.position = Vector3.Lerp(transform.position, target.position - offset, speed * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, speed * Time.deltaTime);
	}
}

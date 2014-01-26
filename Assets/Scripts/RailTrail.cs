using UnityEngine;
using System.Collections;

public class RailTrail : MonoBehaviour {
	public Transform railParent;
	public GameObject railPrefab;
	public float railModuleLength;
	
	Vector3 lastSpawnPosition;
	// Use this for initialization
	void Start () {
		lastSpawnPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = transform.position - lastSpawnPosition;
		direction.Normalize();
		float distance = Vector3.Distance(transform.position, lastSpawnPosition);
		int copies = (int) (distance / railModuleLength);
		Debug.Log(copies);
		for(int i = 0; i < copies; i++)
		{
			GameObject instance = Instantiate(railPrefab, lastSpawnPosition + direction * (railModuleLength * (i + .5f)), transform.rotation) as GameObject;
			instance.transform.parent = railParent;
		}
		lastSpawnPosition = transform.position;
	}
}

using UnityEngine;
using System.Collections;

public class RailTrail : MonoBehaviour {
	public Transform railParent;
	public GameObject railPrefab;
	public GameObject gapPrefab;
	public float railModuleLength;
	
	public bool spawnRail;
	
	Vector3 lastSpawnPosition;
	// Use this for initialization
	void Start () {
		lastSpawnPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject spawnPrefab;
		if(spawnRail)
		{
			spawnPrefab = railPrefab;
		}
		else
		{
			spawnPrefab = gapPrefab;
		}
		Vector3 direction = transform.position - lastSpawnPosition;
		direction.Normalize();
		float distance = Vector3.Distance(transform.position, lastSpawnPosition);
		int copies = (int) (distance / railModuleLength);
		for(int i = 0; i < copies; i++)
		{
			GameObject instance = Instantiate(spawnPrefab, lastSpawnPosition + direction * (railModuleLength * (i + .5f)), transform.rotation) as GameObject;
			instance.transform.parent = railParent;
		}
		lastSpawnPosition = transform.position;
	}
}

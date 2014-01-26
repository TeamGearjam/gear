using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailManager : MonoBehaviour {
	public RailTrail top;
	public RailTrail bottom;
	public RailTrail left;
	public RailTrail right;
	
	
	List<RailTrail> activeTrails = new List<RailTrail>();
	
	public float minGapTime = 1f;
	public float maxGapTime = 5f;
	
	public float gapSpawnFrequence = 2f;
	
	float lastGap = 0;
	
	// Use this for initialization
	void Start ()
	{
		activeTrails.Add(top);
		activeTrails.Add(bottom);
		activeTrails.Add(left);
		activeTrails.Add(right);
	}                    
	
	// Update is called once per frame
	void Update ()
	{
		lastGap += Time.deltaTime;
		if(lastGap > gapSpawnFrequence && activeTrails.Count > 2)
		{
			lastGap = 0;
			StartCoroutine("SpawnGap");
		}
	}
	
	IEnumerator SpawnGap()
	{
		float gapTime = Random.Range(minGapTime, maxGapTime);
		int randIndex = Random.Range(0, activeTrails.Count);
		RailTrail buffer = activeTrails[randIndex];
		activeTrails.RemoveAt(randIndex);
		buffer.spawnRail = false;
		yield return new WaitForSeconds(gapTime);
		buffer.spawnRail = true;
		activeTrails.Add(buffer);		
	}
}

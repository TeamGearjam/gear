using UnityEngine;
using System.Collections;

public class PathController : MonoBehaviour {
	public CurvedPath path;
	public bool doFollow;
	public float startDelay;
	float time;
	// Use this for initialization
	void Start () {
		StartCoroutine("DelayStart");
	}
	
	void Update()
	{
		if(doFollow)
		{
			time += Time.deltaTime;
			transform.position = path.EvaluatePath(time);
			transform.rotation = path.EvaluateRotation(time);
		}
	}
	
	IEnumerator DelayStart()
	{
		yield return new WaitForSeconds(startDelay);
		doFollow = true;
	}
}

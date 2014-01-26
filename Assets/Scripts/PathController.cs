using UnityEngine;
using System.Collections;

public class PathController : MonoBehaviour {
	public CurvedPath path;
	public bool doFollow;
	public float startDelay;
	public float speed = 0.1f;
	float time;
	
	void Start () {
		StartCoroutine("DelayStart");
	}
	
	void Update()
	{
		if(doFollow)
		{
			Follow();
		}
	}
	
	public void Reset()
	{
		time = 0;	
	}
	
	void Follow()
	{
		time += speed * Time.deltaTime;
		transform.position = path.EvaluatePath(time);
		transform.rotation = path.EvaluateRotation(time);
	}
	
	IEnumerator DelayStart()
	{
		yield return new WaitForSeconds(startDelay);
		doFollow = true;
	}
}

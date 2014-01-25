using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SplineController))]
public class PathGenerator : MonoBehaviour {
	public float maxSectionAngle = 30.0f;
	public float sectionDistance = 10.0f;
	public float sectionOffset = 2.0f;
	
	public Transform splinePath;
	
	private SplineController splineController;
	
	// Use this for initialization
	void Start () {
		splineController = this.GetComponent<SplineController>();
		for(int i = 0; i < 10; i++)
		{
			NextNode();	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space))
		{
			splineController.FollowSpline();	
		}
	}
	
	void NextNode()
	{
		Transform last = this.transform;
		
		Transform newNode = new GameObject("" + splinePath.childCount).transform;
		
		newNode.transform.position = last.position;
		
		if(splinePath.childCount > 0)
		{
			last = splinePath.GetChild(splinePath.childCount - 1);
			newNode.position = last.position + last.forward * sectionDistance + last.right * Random.Range(-sectionOffset, sectionOffset) + last.up * Random.Range(-sectionOffset, sectionOffset);
		}
		
		newNode.parent = splinePath;
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SplineController))]
public class PathGenerator : MonoBehaviour {
	public float maxSectionAngle = 30.0f;
	public float sectionDistance = 10.0f;
	public float sectionOffset = 2.0f;
	
	public Transform splinePath;
	public CurvedPath curvedPath;
	
	private SplineController splineController;
	
	// Use this for initialization
	void Start () {
		splineController = this.GetComponent<SplineController>();
		for(int i = 0; i < 10; i++)
		{
			AddCurvedNode();	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void AddNode()
	{
		Transform last = this.transform;
		
		Transform newNode = new GameObject("" + splinePath.childCount).transform;
		
		newNode.transform.position = last.position;
		
		if(splinePath.childCount > 0)
		{
			last = splinePath.GetChild(splinePath.childCount - 1);
			newNode.rotation *= Quaternion.Euler(0,0,Random.Range(-maxSectionAngle, maxSectionAngle));
			newNode.position = last.position + last.forward * sectionDistance + last.right * Random.Range(-sectionOffset, sectionOffset) + last.up * Random.Range(-sectionOffset, sectionOffset);
		}
		
		newNode.parent = splinePath;
	}
	
	void AddCurvedNode()
	{
		Transform last = this.transform;
		
		Transform newNode = new GameObject("" + splinePath.childCount).transform;
		
		newNode.transform.position = last.position;
		
		if(splinePath.childCount > 0)
		{
			last = splinePath.GetChild(splinePath.childCount - 1);
			newNode.rotation *= Quaternion.Euler(0,0,Random.Range(-maxSectionAngle, maxSectionAngle));
			newNode.position = last.position + last.forward * sectionDistance + last.right * Random.Range(-sectionOffset, sectionOffset) + last.up * Random.Range(-sectionOffset, sectionOffset);
		}
		
		newNode.parent = splinePath;
		CurvedPath.Point point = new CurvedPath.Point();
		point.position = newNode.position;
		point.rotation = newNode.rotation;
		curvedPath.AddNode(point);
	}
}

using UnityEngine;
using System.Collections.Generic;

public class CurvedPath : MonoBehaviour {
	[System.Serializable]
	public class Point
	{
		public Vector3 position;
		public Quaternion rotation;
	}
	
	public float duration = 10;
	
	public List<Transform> nodes = new List<Transform>();
	
	void OnDrawGizmos()
	{
		for(int i = 1; i < nodes.Count; i++)
		{
			Debug.DrawLine(nodes[i-1].position, nodes[i].position, Color.cyan);	
		}
	}
	
	public void AddNode(Transform node)
	{
		Vector3Curve positionCurve;
		
		if(nodes.Count > 0)
		{
			int lastIndex = nodes.Count - 1;
			
			positionCurve = new Vector3Curve(nodes[lastIndex], node);
			
			for(float i = 0.1f; i < 1.1f; i += 0.1f)
			{
				Transform point = new GameObject("").transform;
				point.position = positionCurve.Evaluate(i);
				point.rotation = positionCurve.EvaluateRotation(i);//Quaternion.Slerp(nodes[lastIndex].rotation, node.rotation, i);
				nodes.Add(point);
			}
		}
		else
		{
			nodes.Add(node);
		}
		Destroy(node.gameObject);
	}
	
	public Vector3 EvaluatePath(float time)
	{
		float precise = (Mathf.Min(time / duration, 1.0f) * (nodes.Count - 1));
		int index = (int) precise;
		Vector3 position = nodes[index].position + (nodes[Mathf.Min(nodes.Count - 1,index + 1)].position - nodes[index].position) * (precise - index);
		return position;	
	}
	
	public Quaternion EvaluateRotation(float time)
	{
		float precise = (Mathf.Min(time / duration, 1.0f) * (nodes.Count - 1));
		int index = (int) precise;
		Vector3 rotation = nodes[index].rotation.eulerAngles;
		if(rotation.z > 180)
		{
			rotation.z -= 360;	
		}
		Vector3 prevRotation = nodes[Mathf.Min(nodes.Count - 1,index + 1)].rotation.eulerAngles;
		if(prevRotation.z > 180)
		{
			prevRotation.z -= 360;
		}
		
		if(rotation.x > 180)
		{
			rotation.x -= 360;	
		}
		if(prevRotation.x > 180)
		{
			prevRotation.x -= 360;
		}
		
		if(rotation.y > 180)
		{
			rotation.y -= 360;	
		}
		if(prevRotation.y > 180)
		{
			prevRotation.y -= 360;
		}
		
		rotation += (prevRotation - rotation) * (precise - index);
		return Quaternion.Euler(rotation);	
	}
}

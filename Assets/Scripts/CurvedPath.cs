using UnityEngine;
using System.Collections.Generic;

public class CurvedPath : MonoBehaviour {
	[System.Serializable]
	public class Point
	{
		public Vector3 position;
		public Quaternion rotation;
	}
	
	public List<Point> nodes = new List<Point>();
	
	void OnDrawGizmos()
	{
		for(int i = 1; i < nodes.Count; i++)
		{
			Debug.DrawLine(nodes[i-1].position, nodes[i].position, Color.cyan);	
		}
	}
	
	public void AddNode(Point node)
	{
		if(nodes.Count > 0)
		{
			int lastIndex = nodes.Count - 1;
			List<Vector3> points = new List<Vector3>();
			if(nodes.Count >= 10)
				points.Add(nodes[lastIndex].position);
			points.Add(nodes[lastIndex].position);
			points.Add(node.position);
			for(float i = 0.1f; i < 1.1f; i += 0.1f)
			{
				float percent = i;
				if(nodes.Count >= 10)
					percent = .5f + i / 2.0f;
				Point point = new Point();
				point.position = iTween.PointOnPath(points.ToArray(), percent);
				//point.position = Vector3.Slerp(nodes[lastIndex].position, node.position, i);
				point.rotation = MathUtils.Slerp(nodes[lastIndex].rotation, node.rotation, percent);
				nodes.Add(point);
			}
		}
		else
		{
			nodes.Add(node);
		}
	}
}

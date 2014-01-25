using UnityEngine;
using System.Collections;

public class Vector3Curve {
	Transform start, end;
	AnimationCurve x, y, z;
	
	public Vector3Curve(Transform start, Transform end)
	{
		this.start = start;
		this.end = end;
		InitializeCurve();
	}
	
	void InitializeCurve()
	{
		x = new AnimationCurve();
		
		Keyframe key;
		
		key = new Keyframe(0, start.position.x);
		key.inTangent = start.forward.x * 10;
		key.outTangent = start.forward.x * 10;
		key.tangentMode = 1;
		x.AddKey(key);
		
		key = new Keyframe(1.0f, end.position.x);
		key.tangentMode = 1;
		key.inTangent = end.forward.x * 10;
		key.outTangent = end.forward.x * 10;
		x.AddKey(key);
		
		y = new AnimationCurve();
		
		key = new Keyframe(0, start.position.y);
		key.tangentMode = 1;
		key.inTangent = start.forward.y* 10;
		key.outTangent = start.forward.y* 10;
		y.AddKey(key);
		
		key = new Keyframe(1.0f, end.position.y);
		key.tangentMode = 1;
		key.inTangent = end.forward.y* 10;
		key.outTangent = end.forward.y* 10;
		y.AddKey(key);
		
		z = new AnimationCurve();
		
		
		key = new Keyframe(0, start.position.z);
		key.tangentMode = 1;
		key.inTangent = start.forward.z* 10;
		key.outTangent = start.forward.z* 10;
		z.AddKey(key);
		
		key = new Keyframe(1.0f, end.position.z);
		key.tangentMode = 1;
		key.inTangent = end.forward.z* 10;
		key.outTangent = end.forward.z* 10;
		z.AddKey(key);
		
	}
	
	public Vector3 Evaluate(float time)
	{
		return new Vector3(x.Evaluate(time), y.Evaluate(time), z.Evaluate(time));
	}
	
	public Quaternion EvaluateRotation(float time)
	{
		Vector3 first, second;
		first = Evaluate(time);
		second = Evaluate(time + .05f);
		return Quaternion.LookRotation(second - first);
		//return Quaternion.Euler(xRot,yRot,zRot);
	}
}

using UnityEngine;
using System.Collections;

public class Vector3Curve {
	Transform start, end;
	float curveFactor;
	AnimationCurve x, y, z;
	
	public Vector3Curve(Transform start, Transform end, float curveFactor)
	{
		this.start = start;
		this.end = end;
		this.curveFactor = curveFactor;
		InitializeCurve();
	}
	
	void InitializeCurve()
	{
		x = new AnimationCurve();
		
		Keyframe key;
		
		key = new Keyframe(0, start.position.x);
		key.inTangent = start.forward.x * curveFactor;
		key.outTangent = start.forward.x * curveFactor;
		key.tangentMode = 1;
		x.AddKey(key);
		
		key = new Keyframe(1.0f, end.position.x);
		key.tangentMode = 1;
		key.inTangent = end.forward.x * curveFactor;
		key.outTangent = end.forward.x * curveFactor;
		x.AddKey(key);
		
		y = new AnimationCurve();
		
		key = new Keyframe(0, start.position.y);
		key.tangentMode = 1;
		key.inTangent = start.forward.y* curveFactor;
		key.outTangent = start.forward.y* curveFactor;
		y.AddKey(key);
		
		key = new Keyframe(1.0f, end.position.y);
		key.tangentMode = 1;
		key.inTangent = end.forward.y* curveFactor;
		key.outTangent = end.forward.y* curveFactor;
		y.AddKey(key);
		
		z = new AnimationCurve();
		
		
		key = new Keyframe(0, start.position.z);
		key.tangentMode = 1;
		key.inTangent = start.forward.z* curveFactor;
		key.outTangent = start.forward.z* curveFactor;
		z.AddKey(key);
		
		key = new Keyframe(1.0f, end.position.z);
		key.tangentMode = 1;
		key.inTangent = end.forward.z* curveFactor;
		key.outTangent = end.forward.z* curveFactor;
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
	}
}

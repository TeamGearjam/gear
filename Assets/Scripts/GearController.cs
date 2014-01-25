using UnityEngine;
using System.Collections;

public class GearController : MonoBehaviour {
	public float jumpTime;
	public float rotSpeed;
	public float laneDistance;
	public Transform gear;
	
	enum Lane { Top, Bottom, Left, Right };
	
	private bool jumping;
	private Lane targetLane = Lane.Left; 
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gear.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.Self);
		if(!jumping)
		{
			if(Input.GetAxisRaw("Horizontal") < 0)
			{
				JumpToLane(Lane.Left);
			}
			else if(Input.GetAxisRaw("Horizontal") > 0)
			{
				JumpToLane(Lane.Right);
			}
			else if(Input.GetAxisRaw("Vertical") < 0)
			{
				JumpToLane(Lane.Bottom);
			}
			else if(Input.GetAxisRaw("Vertical") > 0)
			{
				JumpToLane(Lane.Top);
			}
		}
	}
	
	void JumpToLane(Lane lane)
	{
		jumping = true;
		targetLane = lane;
		StartCoroutine("Jump");
	}
	
	IEnumerator Jump()
	{
		Vector3 targetPos = Vector3.zero;
		float targetRot = 0;
		switch(targetLane)
		{
		case Lane.Bottom:
			targetPos = new Vector3(0, -laneDistance,0);
			targetRot = 90;
			break;
		case Lane.Top:
			targetPos = new Vector3(0, laneDistance,0);
			targetRot = 90;
			break;
		case Lane.Left:
			targetPos = new Vector3(-laneDistance, 0, 0);
			targetRot = 0;
			break;
		case Lane.Right:
			targetPos = new Vector3(laneDistance, 0, 0);
			targetRot = 0;
			break;
		default:
			break;
		}
		
		for(float i = 0.0f; i < jumpTime; i += Time.deltaTime)
		{
			float percent = i / jumpTime;
			transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, percent);
			Vector3 rot = transform.localRotation.eulerAngles;
			rot.z = Mathf.Lerp(rot.z, targetRot, percent);
			transform.localRotation = Quaternion.Euler(rot);
			yield return null;	
		}
		jumping = false;
	}
}

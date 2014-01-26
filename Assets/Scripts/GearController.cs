using UnityEngine;
using System.Collections;

public class GearController : MonoBehaviour {
	public float jumpTime;
	public float rotSpeed;
	public float laneDistance;
	public Transform gear;
	public Transform particle;
	
	enum Lane { Top, Bottom, Left, Right };
	
	private bool jumping;
	private Lane currentLane = Lane.Left;
	private Lane targetLane = Lane.Left; 
	private bool reverse = true;
	
	// Use this for initialization
	void Start () {
		transform.localPosition = Vector3.left * laneDistance;
	}
	
	// Update is called once per frame
	void Update () {
		gear.Rotate(0, rotSpeed * Time.deltaTime * (reverse ? -1.0f : 1.0f), 0, Space.Self);
		if(!jumping)
		{
			if(Input.GetAxisRaw("Horizontal") < 0 && currentLane != Lane.Left)
			{
				JumpToLane(Lane.Left);
			}
			else if(Input.GetAxisRaw("Horizontal") > 0 && currentLane != Lane.Right)
			{
				JumpToLane(Lane.Right);
			}
			else if(Input.GetAxisRaw("Vertical") < 0 && currentLane != Lane.Bottom)
			{
				JumpToLane(Lane.Bottom);
			}
			else if(Input.GetAxisRaw("Vertical") > 0 && currentLane != Lane.Top)
			{
				JumpToLane(Lane.Top);
			}
		}
	}
	
	void JumpToLane(Lane lane)
	{
		jumping = true;
		particle.gameObject.SetActive(false);
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
			targetRot = 270;
			reverse = false;
			particle.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			break;
		case Lane.Top:
			targetPos = new Vector3(0, laneDistance,0);
			targetRot = 90;
			reverse = false;
			particle.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			break;
		case Lane.Left:
			targetPos = new Vector3(-laneDistance, 0, 0);
			targetRot = 0;
			reverse = true;
			particle.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			break;
		case Lane.Right:
			targetPos = new Vector3(laneDistance, 0, 0);
			targetRot = 180;
			reverse = true;
			particle.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			break;
		default:
			break;
		}
		
		for(float i = 0.0f; i < jumpTime; i += Time.deltaTime)
		{
			float percent = i / jumpTime;
			transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, percent);
			Vector3 rot = transform.localRotation.eulerAngles;
			if(rot.z - targetRot > 180)
			{
				rot.z -= 360;	
			}
			else if(targetRot - rot.z > 180)
			{
				targetRot -= 360;
			}
			rot.z = Mathf.Lerp(rot.z, targetRot, percent);
			transform.localRotation = Quaternion.Euler(rot);
			if(percent < 1)
			{
				yield return null;
			}
		}
		jumping = false;
		currentLane = targetLane;
		particle.gameObject.SetActive(true);
	}
}

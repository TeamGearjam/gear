using UnityEngine;
using System.Collections;

public class GearController : MonoBehaviour {
	public float jumpTime;
	public float rotSpeed;
	public float laneWidth = 3.0f;
	private float gearRadius;
	public float forceFactor;
	public float torqueFactor;
	public Transform gear;
	public Transform particleAnchor;
	public Transform particle;
	public Gear[] gears;
	public int targetGear;
	public float holdTime;
	public PathController pathController;
	public float deathTime = 2f;
	
	enum Lane { Top, Bottom, Left, Right };
	
	private bool jumping;
	private Lane currentLane = Lane.Bottom;
	private Lane targetLane = Lane.Bottom; 
	private bool reverse = true;
	private int currentGear = -1;
	
	private bool dead;
	private bool holding;
	private float hold;
	
	// Use this for initialization
	void Start () {
		Reset();
		SwitchGear();
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead)
		{
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
				
				if(Input.GetButton("SwitchGear") && hold > holdTime)
				{
					targetGear--;
					targetGear = Mathf.Max(targetGear, 0);
					SwitchGear();
					hold = 0;
				}
				else if(Input.GetButtonDown("SwitchGear"))
				{
					holding = true;
					targetGear++;
					targetGear = Mathf.Min(targetGear, gears.Length - 1);
					SwitchGear();
				}
				else if(Input.GetButtonUp("SwitchGear"))
				{
					holding = false;
				}
				
				if(holding)
				{
					hold += Time.deltaTime;
				}
				else
				{
					hold = 0;	
				}
			}
		}
	}
	
	void Reset()
	{
		transform.localPosition = Vector3.down * (laneWidth - gearRadius);
		transform.localRotation = Quaternion.Euler(0, 0, 270);
		currentLane = Lane.Bottom;
		targetLane = Lane.Bottom;
		reverse = false;
		jumping = false;
		particleAnchor.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
		
		pathController.Reset();
	}
	
	void JumpToLane(Lane lane)
	{
		jumping = true;
		particleAnchor.gameObject.SetActive(false);
		targetLane = lane;
		StopCoroutine("Jump");
		StartCoroutine("Jump");
	}
	
	void SwitchGear()
	{
		currentGear = targetGear;
		for(int i = 0; i < gears.Length; i++)
		{
			gears[i].gameObject.SetActive(false);	
		}
		gears[currentGear].gameObject.SetActive(true);
		
		gearRadius = gears[currentGear].radius;
		
		pathController.speed = gears[currentGear].speed;
		
		particle.localPosition = new Vector3(gearRadius, 0, 0);
		
		Vector3 targetPos = Vector3.zero;
		switch(currentLane)
		{
		case Lane.Bottom:
			targetPos = new Vector3(0, -(laneWidth - gearRadius),0);
			break;
		case Lane.Top:
			targetPos = new Vector3(0, (laneWidth - gearRadius),0);
			break;
		case Lane.Left:
			targetPos = new Vector3(-(laneWidth - gearRadius), 0, 0);
			break;
		case Lane.Right:
			targetPos = new Vector3((laneWidth - gearRadius), 0, 0);
			break;
		default:
			break;
		}
		transform.localPosition = targetPos;
	}
	
	IEnumerator Jump()
	{
		Vector3 targetPos = Vector3.zero;
		float targetRot = 0;
		switch(targetLane)
		{
		case Lane.Bottom:
			targetPos = new Vector3(0, -(laneWidth - gearRadius),0);
			targetRot = 270;
			reverse = false;
			particleAnchor.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			break;
		case Lane.Top:
			targetPos = new Vector3(0, (laneWidth - gearRadius),0);
			targetRot = 90;
			reverse = false;
			particleAnchor.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			break;
		case Lane.Left:
			targetPos = new Vector3(-(laneWidth - gearRadius), 0, 0);
			targetRot = 0;
			reverse = true;
			particleAnchor.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			break;
		case Lane.Right:
			targetPos = new Vector3((laneWidth - gearRadius), 0, 0);
			targetRot = 180;
			reverse = true;
			particleAnchor.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
			if(percent > .5f)
			{
				jumping = false;
				currentLane = targetLane;
				particleAnchor.gameObject.SetActive(true);
			}
			yield return null;
		}
	}
	
	public void Gap()
	{
		StopCoroutine("Jump");
		StartCoroutine("Death");
	}
	
	IEnumerator Death()
	{
		SetDead(true);
		yield return new WaitForSeconds(deathTime);
		SetDead(false);
		Reset();
	}
	
	void SetDead(bool state)
	{
		if(dead != state)
		{
			dead = state;
			particleAnchor.gameObject.SetActive(!state);
			pathController.doFollow = !state;
			rigidbody.isKinematic = !state;
			rigidbody.useGravity = state;
			if(state)
			{
				rigidbody.AddForce(transform.forward * pathController.speed * forceFactor,ForceMode.VelocityChange);
				rigidbody.AddTorque(-transform.up * rotSpeed / torqueFactor, ForceMode.VelocityChange);
			}
		}
	}
}

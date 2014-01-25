using UnityEngine;
using System.Collections;

public class GearController : MonoBehaviour {
	public float rotSpeed;
	public float laneDistance;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, rotSpeed, 0, Space.Self);
		if(Input.GetAxisRaw("Horizontal") < 0)
		{
			transform.localPosition = new Vector3(-laneDistance, 0,0);
		}
		else if(Input.GetAxisRaw("Horizontal") > 0)
		{
			transform.localPosition = new Vector3(laneDistance, 0,0);
		}
		if(Input.GetAxisRaw("Vertical") < 0)
		{
			transform.localPosition = new Vector3(0, -laneDistance,0);
		}
	}
}

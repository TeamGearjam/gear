using UnityEngine;
using System.Collections;

public class Gear : MonoBehaviour {
	public GearController controller;
	public float radius;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider col)
	{
		controller.Gap();	
	}
}

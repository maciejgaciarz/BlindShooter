using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanging : MonoBehaviour {

	public float minimumY = 15.0f;
	public float maximumY = 50.0f;

	public float minimumZ = 5.0f;
	public float maximumZ = 120.0f;

	static float t = 0.0f;

	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = new Vector3 (0, Mathf.Lerp(minimumY,maximumY,t),Mathf.Lerp(minimumZ,maximumZ,t));

		t =+ 10.0f * Time.deltaTime;
	

	if(t > 1.0f)
	{
		float tempY = maximumY;
		float tempZ = maximumZ;

		maximumY = minimumY;
		minimumZ = tempZ;
				
		maximumZ = minimumZ;
		minimumZ=tempZ;
		t=0.0f;
	}
	}
}

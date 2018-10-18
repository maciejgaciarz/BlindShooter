using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if (Application.isEditor) {
			Debug.Log ("Apka odpalona przez editor");
		} else if (SystemInfo.deviceType == DeviceType.Desktop) {
			Debug.Log ("Apka odpalona na Pececie");
		} else if (SystemInfo.deviceType == DeviceType.Handheld) {
			Debug.Log ("Apka odpalona na telefonie/tablecie(na handheldzie)");
		}
	}
}

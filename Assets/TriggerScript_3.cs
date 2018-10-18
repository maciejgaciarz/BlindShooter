using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript_3 : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("3");

        if (other.gameObject.tag == "bullet")
        {
            other.SendMessage("SetScore", 5);
        }

    }
}

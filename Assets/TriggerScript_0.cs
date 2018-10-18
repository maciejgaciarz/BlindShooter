using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript_0 : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("0");
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("0!!");
            // other.SendMessage("SetScore", 10);
            other.GetComponent<BulletLife>().SetScore(2);
        }
        
    }
}

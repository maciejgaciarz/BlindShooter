using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript_1 : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");

        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("1!!");
            other.SendMessage("SetScore", 3);
            //other.GetComponent<BulletLife>().SetScore(20);
        }

    }
}

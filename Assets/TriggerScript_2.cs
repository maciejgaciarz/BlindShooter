using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript_2 : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("2");

        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("2!!");
            other.SendMessage("SetScore", 4);
            //  other.GetComponent<BulletLife>().SetScore(30);
        }

    }
}

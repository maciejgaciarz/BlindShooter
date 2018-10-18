using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPLayer : MonoBehaviour {

    private AI_ManagerScript aiReference;

    private void Start()
    {
        aiReference = GameObject.FindGameObjectWithTag("AI_Manager").GetComponent<AI_ManagerScript>();

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PP");
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("PP!!");
           
           // aiReference.cpuCanHitYouNow = true;
            //aiReference.cpuCanHitYouNowV3 = other.gameObject.GetComponent<BulletLife>().myDestination;

            other.GetComponent<BulletLife>().SetScore(50);
        }

    }
}

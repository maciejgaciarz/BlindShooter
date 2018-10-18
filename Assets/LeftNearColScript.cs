using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNearColScript : MonoBehaviour {

    [SerializeField]
    private GameObject leftScoreBoard;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("TurnOnScoreBoard");
    }

    IEnumerator TurnOnScoreBoard()
    {
        leftScoreBoard.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        leftScoreBoard.SetActive(false);
    }
}

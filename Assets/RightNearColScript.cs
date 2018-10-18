using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightNearColScript : MonoBehaviour {

    [SerializeField]
    private GameObject rightScoreBoard;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("TurnOnScoreBoard");
    }

    IEnumerator TurnOnScoreBoard()
    {
        rightScoreBoard.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        rightScoreBoard.SetActive(false);
    }
}

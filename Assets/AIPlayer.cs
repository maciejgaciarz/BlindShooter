using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private GameObject hitText;

    void Awake()
    {
        health = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health -= 25;
            StartCoroutine("ShowHitText");
            if (health < 25)
            {
                Rounds.Instance.IncrementAIRounds(false);
            }
        }
    }

    IEnumerator ShowHitText()
    {
        hitText.SetActive(true);
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(1);
        hitText.SetActive(false);
    }

    public void RestartHP()
    {
        health = 100;      
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLife : MonoBehaviour
{

    public Vector3 myDestination;
    public int myScore = 1;
    [SerializeField]
    private float damage = 25f;

    void Awake()
	{      
        StartCoroutine("Die");
	}

    public void SetScore(int _score)
    {
        myScore = _score;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<SinglePlayer>().GetShot(damage);
        }
    }
}

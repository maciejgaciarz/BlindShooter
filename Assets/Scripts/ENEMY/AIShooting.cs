using UnityEngine;
using System.Collections;

public class AIShooting : MonoBehaviour
{

    public Rigidbody projectile;

    [SerializeField]
    public float speed = 100;
    [SerializeField]
    float TimeBetweenShots;

    bool isShooting = false;
    /*
    IEnumerator Shooting()
    {
        while(isShooting)
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            yield return new WaitForSeconds(TimeBetweenShots);
        }   
    }
    private void Start()
    {
        isShooting = true;
        TimeBetweenShots = Random.Range(1f, 2f);
        StartCoroutine(Shooting());
    }
    */


}
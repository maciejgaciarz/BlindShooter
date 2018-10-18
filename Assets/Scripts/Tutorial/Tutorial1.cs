using UnityEngine;
using System.Collections;

public class PlayerShootings : MonoBehaviour
{

    public ParticleSystem muzzleFlash;
    Animator anim;
    public GameObject impactPrefab;

    GameObject[] impacts;
    int currentImpact = 0;
    int maxImpacts = 5;

    bool shooting = false;

    // Use this for initialization
    void Start()
    {

        impacts = new GameObject[maxImpacts];
        for (int i = 0; i < maxImpacts; i++)
            impacts[i] = (GameObject)Instantiate(impactPrefab);

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && !Input.GetKey(KeyCode.LeftShift))
        {
            muzzleFlash.Play();
            anim.SetTrigger("Fire");
            shooting = true;
        }

    }

    void FixedUpdate()
    {
        if (shooting)
        {
            shooting = false;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
            {
                if (hit.transform.tag == "Enemy")
                    Destroy(hit.transform.gameObject);

                impacts[currentImpact].transform.position = hit.point;
                impacts[currentImpact].GetComponent<ParticleSystem>().Play();

                if (++currentImpact >= maxImpacts)
                    currentImpact = 0;
            }
        }
    }
}
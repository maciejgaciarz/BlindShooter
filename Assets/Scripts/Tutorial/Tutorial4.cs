using UnityEngine;
using System.Collections;

public class PlayerShootingssd : MonoBehaviour
{

    public ParticleSystem muzzleFlash;
    public GameObject impactPrefab;

    Animator anim;
    GameObject[] impacts;
    int currentImpact = 0;
    int maxImpacts = 5;
    bool shooting = false;
    float damage = 25f;


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
                //print (hit.transform.name);

                if (hit.transform.tag == "Player")
                {
                    hit.transform.GetComponent<PhotonView>().RPC("GetShot", PhotonTargets.All, damage);
                }

                //cycle impact effects
                impacts[currentImpact].transform.position = hit.point;
                impacts[currentImpact].GetComponent<ParticleSystem>().Play();

                if (++currentImpact >= maxImpacts)
                    currentImpact = 0;
            }
        }
    }
}
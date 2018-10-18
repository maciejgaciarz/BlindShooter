using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public ParticleSystem muzzleFlash;
    public GameObject impactPrefab;

    Animator anim;
    GameObject[] impacts;
    int currentImpact = 0;
    int maxImpacts = 5;
    float damage = 25f;
    bool shooting = false;

    void Start()
    {
        impacts = new GameObject[maxImpacts];
        for (int i = 0; i < maxImpacts; i++)
            impacts[i] = (GameObject)Instantiate(impactPrefab);

        anim = GetComponentInChildren<Animator>();
    }

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
                if (hit.transform.tag == "Player")
                    hit.transform.GetComponent<PhotonView>().RPC("GetShot", PhotonTargets.All, damage, PhotonNetwork.player.name);

                impacts[currentImpact].transform.position = hit.point;
                impacts[currentImpact].GetComponent<ParticleSystem>().Play();

                if (++currentImpact >= maxImpacts)
                    currentImpact = 0;
            }
        }
    }
}
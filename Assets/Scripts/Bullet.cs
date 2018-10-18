using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 myDestination;
    public int myScore = 0;
    private float damage = 25f;
    private AI_ManagerScript aiReference;
    public string ownerName;

    void Awake()
    {
        StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && ownerName != col.gameObject.name)
        {
            col.gameObject.GetComponent<NetworkPlayer>().ActivateShowHitText();
            col.transform.GetComponent<PhotonView>().RPC("GetShot", PhotonTargets.Others, damage, PhotonNetwork.otherPlayers[0].NickName);
        }
    }
}

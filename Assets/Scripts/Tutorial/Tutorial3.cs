using UnityEngine;
using System.Collections;

public class PlayerNetworkMover : Photon.MonoBehaviour
{

    public delegate void Respawn(float time);
    public event Respawn RespawnMe;

    Vector3 position;
    Quaternion rotation;
    float smoothing = 10f;
    float health = 100f;


    void Start()
    {

        if (photonView.isMine)
        {
            //rigidbody.useGravity = true;
            //GetComponent<FirstPersonCharacter>().enabled = true;
            //GetComponent<FirstPersonHeadBob>().enabled = true;
            //GetComponent<SimpleMouseRotator>().enabled = true;
            //GetComponentInChildren<PlayerShooting>().enabled = true;
            //foreach (SimpleMouseRotator rot in GetComponentsInChildren<SimpleMouseRotator>())
            //    rot.enabled = true;
            //foreach (Camera cam in GetComponentsInChildren<Camera>())
            //    cam.enabled = true;

           // transform.Find("Head Joint/First Person Camera/GunCamera/Candy-Cane").gameObject.layer = 11;
        }
        else
        {
            StartCoroutine("UpdateData");
        }
    }

    IEnumerator UpdateData()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);
            yield return null;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(health);
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            health = (float)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void GetShot(float damage)
    {
        health -= damage;
        if (health <= 0 && photonView.isMine)
        {
            if (RespawnMe != null)
                RespawnMe(3f);

            PhotonNetwork.Destroy(gameObject);
        }
    }

}
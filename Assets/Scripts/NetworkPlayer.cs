using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class NetworkPlayer : Photon.MonoBehaviour
{
    [SerializeField]
    private GameObject hitText;
    public delegate void Respawn(float time);
    public event Respawn RespawnMe;
    private Vector3 position;
    private Quaternion rotation;
    private float smoothing = 10f;
    [SerializeField]
    private float health = 100f;
    private bool initialLoad = true;

    void Start()
    {
        if (photonView.isMine)
        {
            GameObject.Find("PlayerHP").GetComponent<Text>().text = "HP: " + health.ToString();
            GetComponent<Camera>().enabled = true;
            GetComponent<SimpleMouseRotator>().enabled = true;
            GetComponent<Shooting>().enabled = true;
        }
    }

    IEnumerator UpdateData()
    {
        if (initialLoad)
        {
            initialLoad = false;
            transform.rotation = rotation;
        }

        while (true)
        {
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
            //stream.SendNext(health);
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            //health = (float)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void GetShot(float damage,string enemyName)
    {
        if (enemyName == PhotonNetwork.player.NickName)
        {
            Debug.Log("NICKNAME on getshot!!! " + enemyName);
            //StartCoroutine("ShowHitText");
            health -= damage;
            GameObject.Find("PlayerHP").GetComponent<Text>().text = "HP: " + health.ToString();
            if (health <= 0 && photonView.isMine)
            {
                PhotonView[] photonViews = FindObjectsOfType(typeof(PhotonView)) as PhotonView[];
                photonViews[0].RPC("RestartHP", PhotonTargets.All);
                photonViews[0].RPC("IncrementRound", PhotonTargets.All);
                photonViews[0].RPC("IncrementPoint", PhotonTargets.Others);

                //Rounds.Instance.IncrementMultiRounds();
                if (!photonView.isMine)
                {
                    
                }
            }

        }
    }

    public void ActivateShowHitText()
    {
        StartCoroutine("ShowHitText");
    }

    IEnumerator ShowHitText()
    {
        hitText.SetActive(true);
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(1);
        hitText.SetActive(false);
    }
    [PunRPC]
    public void RestartHP()
    {
        health = 100;
        GameObject.Find("PlayerHP").GetComponent<Text>().text = "HP:" + health.ToString();
    }

    [PunRPC]
    public void IncrementRound()
    {
        if (Rounds.Instance.round < 6)
        {
            Rounds.Instance.round++;
            Rounds.Instance.TextUpdates();
        }
        else
        {
            Rounds.Instance.EndMultiGame();
        }

    }

    [PunRPC]
    public void IncrementPoint()
    {
        Rounds.Instance.playerPoints++;
        Rounds.Instance.TextUpdates();

    }
}

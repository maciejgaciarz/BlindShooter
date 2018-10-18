using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManagers : MonoBehaviour
{

    [SerializeField]
    Text connectionText;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    Camera sceneCamera;

    GameObject player;

    void Start()
    {

        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.2");

    }

    void Update()
    {
        connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    void OnJoinedLobby()
    {
        RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom("Mike", ro, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        StartSpawnProcess(0f);
    }

    void StartSpawnProcess(float respawnTime)
    {
        sceneCamera.enabled = true;
        StartCoroutine("SpawnPlayer", respawnTime);
    }

    IEnumerator SpawnPlayer(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);

        int index = Random.Range(0, spawnPoints.Length);
        player = PhotonNetwork.Instantiate("FPSPlayer",
                                           spawnPoints[index].position,
                                           spawnPoints[index].rotation,
                                           0);
        player.GetComponent<PlayerNetworkMover>().RespawnMe += StartSpawnProcess;
        sceneCamera.enabled = false;
    }
}
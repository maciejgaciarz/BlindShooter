using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.PunBehaviour
{
    [SerializeField]
    private Text connectionText;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject sceneCamera;
    [SerializeField]
    private PhotonLogLevel Loglevel = PhotonLogLevel.Full;
    [SerializeField]
    private string _gameVersion = "1";

    private GameObject player;


    void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;

    }
    void Start()
    {
        Connect();
    }

    public void Connect()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.2");
    }

    void Update()
    {
        connectionText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("Mike", ro, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        sceneCamera.SetActive(false);
        StartSpawnProcess(0f);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
        RoomOptions ro = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("Jack", ro, TypedLobby.Default);
    }
    void StartSpawnProcess(float respawnTime)
    {
        sceneCamera.SetActive(false);
        StartCoroutine("SpawnPlayer", respawnTime);
    }

    IEnumerator SpawnPlayer(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        int numberofP = PhotonNetwork.playerList.Length;
        player = PhotonNetwork.Instantiate("FPSPlayer",
                                           spawnPoints[numberofP - 1].position,
                                           spawnPoints[numberofP - 1].rotation,
                                           0);
        player.gameObject.name = "FPSPlayer" + numberofP;
        PhotonNetwork.player.NickName = "FPSPlayer" + numberofP.ToString();
        player.GetComponent<NetworkPlayer>().RespawnMe += StartSpawnProcess;
        sceneCamera.SetActive(false);
    }
}

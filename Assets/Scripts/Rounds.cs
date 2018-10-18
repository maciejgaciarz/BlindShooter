using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rounds : Singleton<Rounds>
{
    [SerializeField]
    private int AmountsOfRounds = 5;
    [SerializeField]
    private Text roundsText;
    [SerializeField]
    private Text pointsText;
    [SerializeField]
    public int round = 0;
    [SerializeField]
    public int playerPoints = 0;
    [SerializeField]
    private int opponentPoint = 0;

    [SerializeField]
    private SinglePlayer singlePlayer;
    [SerializeField]
    private AIPlayer aiPlayer;


    private int player1Points;
    private int player2Points;

    private void Awake()
    {
        playerPoints = 0;
        round = 0;
    }

    public void IncrementAIRounds(bool AIWin)
    {
        AIResetPlayersHP();
        AI_Manager_New.Instance._randomShootingProcess = true;
        AI_Manager_New.Instance.nextShoot = Vector3.zero;
        round++;
        IncrementAIPoints();
        if (!AIWin)
        {
            playerPoints++;
        }
        else
        {
            opponentPoint++;
        }
        SinglePlayer.Instance.RestartHP();
        if (round == 6)
        {
            EndAIGame();
        }
        IncrementAIPoints();
    }

    public void IncrementAIPoints()
    {
        roundsText.text = "Round:" + round.ToString();
        pointsText.text = "Points:" + playerPoints.ToString();
    }

    private void EndAIGame()
    {
        SceneManager.LoadScene(0);
    }

    public void IncrementMultiRounds()
    {
        //MultiResetPlayersHP();
        //round++;
        //playerPoints++;
        UpdatePlayerPoints();               /// Przeczucic to zaleznie od tego kto dostał??
        if (round == 6)
        {
            EndMultiGame();
        }
    }

    public void IncrementMultiPlayerPoints()
    {
        playerPoints++;
        UpdatePlayerPoints();
    }

    private void UpdatePlayerPoints()
    {
        roundsText.text = "Round:" + round.ToString();
        pointsText.text = "Points:" + playerPoints.ToString();
    }

    public void EndMultiGame()
    {
        SceneManager.LoadScene(0);
    }

    public void AIResetPlayersHP()
    {
        singlePlayer.RestartHP();
        aiPlayer.RestartHP();
    }

    public void MultiResetPlayersHP()
    {
        PhotonView[] photonViews = FindObjectsOfType(typeof(PhotonView)) as PhotonView[];
        photonViews[0].RPC("RestartHP", PhotonTargets.All);
        photonViews[0].RPC("IncrementRound", PhotonTargets.All);
        photonViews[0].RPC("IncrementPoint", PhotonTargets.Others);
    }

    public void TextUpdates()
    {
        roundsText.text = "Rounds: " + round.ToString();
        pointsText.text = "Points: " + playerPoints.ToString();  
    }

}

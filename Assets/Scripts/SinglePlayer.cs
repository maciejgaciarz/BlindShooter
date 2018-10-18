using UnityEngine;
using UnityEngine.UI;

public class SinglePlayer : Singleton<SinglePlayer>
{
    private string PlayerHP;
    private float health = 100f;

    public void GetShot(float damage)
    {
        health -= damage;
        GameObject.Find("PlayerHP").GetComponent<Text>().text = "HP:" +  health.ToString();
        if (health == 0)
        {
            Rounds.Instance.IncrementAIRounds(true);
            //Rounds.Instance.IncrementAIPoints();
        }
    }

    public void RestartHP()
    {
        health = 100;
        GameObject.Find("PlayerHP").GetComponent<Text>().text = "HP:" + health.ToString();
    }
}

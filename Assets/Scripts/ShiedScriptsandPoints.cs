using UnityEngine;

public class ShiedScriptsandPoints : MonoBehaviour {

    [SerializeField]
    private int _score;

    [SerializeField]
    private AI_Manager_New.NextShootDesitnation _myWay;

    private Vector3 _myDestination;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trafione pole ->"+_score);
                
            if (other.gameObject.tag == "Bullet")
            {
                if (AI_Manager_New.Instance._bestScore < _score)
                {
                    Debug.Log("shield");
                    AI_Manager_New.Instance._randomShootingProcess = false;
                    AI_Manager_New.Instance.DESTINATION = _myWay;
                    AI_Manager_New.Instance.ChangeOffset();

                    _myDestination = other.GetComponent<BulletLife>().myDestination;
                    AI_Manager_New.Instance._bestDestination = _myDestination;
                    AI_Manager_New.Instance._bestScore = _score;
                }
            }       
    }
}

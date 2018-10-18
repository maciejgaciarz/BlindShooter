using UnityEngine;
using System.Collections;
public class Rotation : MonoBehaviour {
    [SerializeField]
    GameObject Target;

    Vector3 currentEuler;
    float RandomY;
    float speed;
    bool isGoing = true;
    float lerpTime = 5f;
    float currentLerpTime = 0f;
    void Start () {
        currentEuler = Target.transform.eulerAngles;
        // transform.eulerAngles = currentEuler;
        RandomY = Random.Range(203f, 223f);
        speed = 20000f;
        lerpTime = 5;
        currentLerpTime = 0;
        StartCoroutine(WaitForRotation());
    }
    IEnumerator WaitForRotation()
    {
          while(isGoing)
           {
               transform.eulerAngles = new Vector3(0, RandomY, 0);
               Vector3 NewEuler = transform.eulerAngles;
               yield return new WaitForSeconds(2);
               Vector3 tempEuler = transform.eulerAngles;
               transform.eulerAngles = Vector3.Slerp(tempEuler, new Vector3(0, RandomY, 0), currentLerpTime / lerpTime);
               yield return new WaitForSeconds(2);
           }
    }
	void Update () {
        currentLerpTime += Time.deltaTime;
        RandomY = Random.Range(220f,200f);
    }
}

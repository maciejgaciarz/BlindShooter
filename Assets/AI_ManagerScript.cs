using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AI_ManagerScript : MonoBehaviour {

    //PULIC

    public Transform[] pointToDoFirstRandomShoot;

    [SerializeField]
    private Rigidbody projectile;

    [SerializeField]
    private Transform placeForInstantiate;

    public float speed;
    //public bool cpuCanHitYouNow = false;
    //public Vector3 cpuCanHitYouNowV3;

    //PRIVATES
   // private int shootsCounter = 0;
    //private Dictionary<Vector3, int> ShootsRecorder = new Dictionary<Vector3, int>();
   // private List<KeyValuePair<Vector3, int>> ShootsRecorder = new List<KeyValuePair<Vector3, int>>();
   // private int coroutineIterator = 0;
   // private bool randomShootingProceesInProgress = true;
   // private int k_ForKnnKlassyfier = 5;
   // private float incrementer = 0.001f;
    
/*
    public void AddToShootsRecorder(Vector3 v3,int i)
    {
       
        KeyValuePair<Vector3, int> newData = new KeyValuePair<Vector3, int>(v3,i);

        ShootsRecorder.Add(newData);

        foreach (var item in ShootsRecorder)
        {
            Debug.Log(" n = " + item.Key + " v = " + item.Value);
        }
    }
   
    private void Start ()
    {
       // StartCoroutine("RandomShoot");     
	}

    private void Update()
    {
        if(randomShootingProceesInProgress == false)
        {
            StartCoroutine("turnOnCor");
            randomShootingProceesInProgress = true;
        }
    }

    IEnumerator RandomShoot()
    {

        yield return new WaitForSeconds(2f);

        //values for first shoot
        float z = Random.Range(pointToDoFirstRandomShoot[0].position.z, pointToDoFirstRandomShoot[1].position.z);
        float y = Random.Range(pointToDoFirstRandomShoot[0].position.y, pointToDoFirstRandomShoot[2].position.y);

        TakeAShoot(y, z);

        if(coroutineIterator < 25)
        {
            coroutineIterator++;
            StartCoroutine("RandomShoot");       
        }
        else
        {
            randomShootingProceesInProgress = false;
        }

    }


    int bufferForBestValue;
    int howManyPositionsINArrayHaveTheBestValues =0;
    List<int> best5;
    Vector3 h;
    void TakeAShoot(float y,float z)
    {
        //position for first shoot
        Vector3 firstRandomShootFinalPosition = new Vector3(pointToDoFirstRandomShoot[0].position.x, y, z);

        //creating a bullet
        Rigidbody instantiatedProjectile = Instantiate(projectile, placeForInstantiate.position, placeForInstantiate.rotation) as Rigidbody;

        //bullet has now destination in his own class, it will be send back with a score later
        instantiatedProjectile.gameObject.GetComponent<BulletLife>().myDestination = firstRandomShootFinalPosition;

        if(true)
        {
            //turn on this shit 
            instantiatedProjectile.velocity = transform.TransformDirection(firstRandomShootFinalPosition);
        }      
        else if(false)
        {

            Vector3 usredniona, zrandomizowana;
            

            for (int i = 0; i < k_ForKnnKlassyfier; i++)
            {
                if(i==0)
                {
                    bufferForBestValue = ShootsRecorder.ElementAt(0).Value;
                }
                if(i > 0 &&  (bufferForBestValue == ShootsRecorder.ElementAt(i).Value))
                {
                    howManyPositionsINArrayHaveTheBestValues++;
                }
            }

            
            for (int i = 0; i < howManyPositionsINArrayHaveTheBestValues; i++)
            {
                h += ShootsRecorder.ElementAt(i).Key;
            }

            usredniona = h / howManyPositionsINArrayHaveTheBestValues;

            zrandomizowana = new Vector3(usredniona.x,usredniona.y,usredniona.z);

            zrandomizowana.y += Random.Range(usredniona.y -1, usredniona.y + 1);
            zrandomizowana.z += Random.Range(usredniona.z - 1, usredniona.z + 1);

            //turn on this shit 
            StartCoroutine("TakeAShot2",zrandomizowana);

            instantiatedProjectile.velocity = transform.TransformDirection(usredniona);
        }
        else if(false)
        {
            //turn on this shit 
            instantiatedProjectile.velocity = transform.TransformDirection(firstRandomShootFinalPosition);
        }
    }
    /*
    IEnumerator TakeAShot2(Vector3 destination)
    {
        yield return new WaitForSeconds(1f);
        Rigidbody instantiatedProjectile2 = Instantiate(projectile, placeForInstantiate.position, placeForInstantiate.rotation) as Rigidbody;

        //bullet has now destination in his own class, it will be send back with a score later
        instantiatedProjectile2.gameObject.GetComponent<BulletLife>().myDestination = destination;

        instantiatedProjectile2.velocity = transform.TransformDirection(destination);
    }
    */
    /*
    IEnumerator turnOnCor()
    {
        Knn();
        yield return new WaitForSeconds(3f);
        StartCoroutine("turnOnCor");
    }

    void Knn()
    {
        // sort our scoreBoard
        var items = from pair in ShootsRecorder
                    orderby pair.Value descending
                    select pair;

        //only DEBUG
        //foreach (KeyValuePair<Vector3, int> pair in items)
        //{
        //   Debug.Log("{0}:"+ pair.Key+" { 1} :"+pair.Value);
        //}

        // Our Best 5 depends of k parameter
        int[] FiveBestResultsValue = new int[k_ForKnnKlassyfier];
        float[] FiveBestResultsCoorY = new float[k_ForKnnKlassyfier];
        float[] FiveBestResultsCoorZ = new float[k_ForKnnKlassyfier];

        //fullfill arrays of values
        for (int i = 0; i < k_ForKnnKlassyfier; i++)
        {
            FiveBestResultsCoorY[i] = items.ElementAt(i).Key.y;
            FiveBestResultsCoorZ[i] = items.ElementAt(i).Key.z;

            FiveBestResultsValue[i] = items.ElementAt(i).Value;
        }

        // weighted average (srednia wazona)
        float newY =0, newZ=0;
        for (int i = 0; i < k_ForKnnKlassyfier; i++)
        {
            // newY += FiveBestResultsCoorY[i];// FiveBestResultsValue[i];
            // newZ += FiveBestResultsCoorZ[i];// * FiveBestResultsValue[i];
            newY += FiveBestResultsCoorY[i]* FiveBestResultsValue[i];
            newZ += FiveBestResultsCoorZ[i]* FiveBestResultsValue[i];
        }

        // fullfill the strength of Divider for weighted average
        int forDivide =0;
        foreach (var item in FiveBestResultsValue)
        {
            forDivide += item;
        }

        //final Values to next Shot 
        //float YAfterKNN = newY / 5;//newY / forDivide;
        //float ZAfterKNN = newZ / 5;//newZ / forDivide;

        float YAfterKNN = newY / forDivide;
        float ZAfterKNN = newZ / forDivide;



        Debug.Log(" ile ->"+ ShootsRecorder.Count);
        float y = ShootsRecorder.ElementAt(0).Key.y;
        float z = ShootsRecorder.ElementAt(0).Key.z;
        TakeAShoot(y,z);
    */
}

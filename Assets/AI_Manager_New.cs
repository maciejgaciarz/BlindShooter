using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager_New : Singleton<AI_Manager_New>
{
    //public
    [SerializeField]
    private Transform[] _pointsAreaToShoot;

    [SerializeField]
    private Rigidbody _projectile;

    [SerializeField]
    private Transform _placeForInstantiate;

    public enum NextShootDesitnation
    {
        Down0, Down1, Down2, Down3, Down4, Down5, Down6, Down7, Down8,
        Left0, Left1, Left2, Left3, Left4, Left5, Left6, Left7, Left8,
        Right0, Right1, Right2, Right3, Right4, Right5, Right6, Right7, Right8,
        Up0, Up1, Up2, Up3, Up4, Up5, Up6, Up7, Up8,

        RightDown0, RightDown1, RightDown2, RightDown3, RightDown4, RightDown5, RightDown6, RightDown7, RightDown8,
        LeftDown0, LeftDown1, LeftDown2, LeftDown3, LeftDown4, LeftDown5, LeftDown6, LeftDown7, LeftDown8,
        RightUp0, RightUp1, RightUp2, RightUp3, RightUp4, RightUp5, RightUp6, RightUp7, RightUp8,
        LeftUp0, LeftUp1, LeftUp2, LeftUp3, LeftUp4, LeftUp5, LeftUp6, LeftUp7, LeftUp8,

        Center

    }

    public NextShootDesitnation DESTINATION;

    public bool _randomShootingProcess = true;

    public Vector3 _bestDestination;
    public Vector3 _perfectDestination;
    
    public int _bestScore = 0;

    //priv
    private float speed;
    public Vector3 nextShoot = Vector3.zero;

    //private float[] offset = { 1.5f, 1.25f, 1, 0.75f, 0.5f, 0.3f, 0.2f, 0.1f, 0.5f };
    private float[] offset = { 4.5f, 3.5f, 2.5f, 2f, 1.5f, 1.3f, 0.9f, 0.6f, 0.3f };
    private float offsetY = 0;
    private float offsetZ = 0;

    private void Start()
    {
        StartCoroutine("RandomShoot");
    }

    IEnumerator RandomShoot()
    {


        yield return new WaitForSeconds(2f);
        //values for first shoot
        float z = Random.Range(_pointsAreaToShoot[0].position.z, _pointsAreaToShoot[1].position.z);
        float y = Random.Range(_pointsAreaToShoot[0].position.y, _pointsAreaToShoot[2].position.y);

        //position for first shoot
        Vector3 firstRandomShootFinalPosition = new Vector3(_pointsAreaToShoot[0].position.x, y, z);

        //creating a bullet
        Rigidbody instantiatedProjectile = Instantiate(_projectile, _placeForInstantiate.position, _placeForInstantiate.rotation) as Rigidbody;

        //bullet has now destination in his own class, it will be send back with a score later
        instantiatedProjectile.gameObject.GetComponent<BulletLife>().myDestination = firstRandomShootFinalPosition;

        //RUN Forest RUN
        instantiatedProjectile.velocity = transform.TransformDirection(firstRandomShootFinalPosition);

        yield return new WaitForSeconds(4f);

        if (_randomShootingProcess == true)
        {
            Debug.Log("111"+_randomShootingProcess);
            StartCoroutine("RandomShoot");
        }
        else
        {
            Debug.Log("222");
            StartCoroutine("ShootingLoop");
        }
    }

    IEnumerator ShootingLoop()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("ShootingLoop");
        Rigidbody instantiatedProjectile = Instantiate(_projectile, _placeForInstantiate.position, _placeForInstantiate.rotation) as Rigidbody;

        //bullet has now destination in his own class, it will be send back with a score later
        if(_perfectDestination.x == 0)
        {
             nextShoot = new Vector3(_bestDestination.x, _bestDestination.y += offsetY, _bestDestination.z += offsetZ);
        }
        else
        {
             nextShoot = _perfectDestination;
        }

        instantiatedProjectile.gameObject.GetComponent<BulletLife>().myDestination = nextShoot;

        instantiatedProjectile.velocity = transform.TransformDirection(nextShoot);

        StartCoroutine("ShootingLoop");
    }



    public void ChangeOffset()
    {
        switch (DESTINATION)
        {
            case NextShootDesitnation.Down0:
                {
                    offsetY =  - offset[0];
                    offsetZ = 0;
                    break;
                }

            case NextShootDesitnation.Down1:
                {
                    offsetY = -offset[1];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Down2:
                {
                    offsetY = -offset[2];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Down3:
                {
                    offsetY = -offset[3];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Down4:
                {
                    offsetY = -offset[4];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Down5:
                {
                    offsetY = -offset[5];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Down6:
                {
                    offsetY = -offset[6];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Down7:
                {
                    offsetY = -offset[7];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Down8:
                {
                    offsetY = -offset[8];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up0:
                {
                    offsetY = offset[0];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up1:
                {
                    offsetY = offset[1];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up2:
                {
                    offsetY = offset[2];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up3:
                {
                    offsetY = offset[3];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up4:
                {
                    offsetY = offset[4];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up5:
                {
                    offsetY = offset[5];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up6:
                {
                    offsetY = offset[6];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up7:
                {
                    offsetY = offset[7];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Up8:
                {
                    offsetY = offset[8];
                    offsetZ = 0;
                    break;
                }
            case NextShootDesitnation.Right0:
                {
                    offsetZ = offset[0];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Right1:
                {
                    offsetZ = offset[1];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Right2:
                {
                    offsetZ = offset[2];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Right3:
                {
                    offsetZ = offset[3];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Right4:
                {
                    offsetZ = offset[4];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Right5:
                {
                    offsetZ = offset[5];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Right6:
                {
                    offsetZ = offset[6];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Right7:
                {
                    offsetZ = offset[7];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Right8:
                {
                    offsetZ = offset[8];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left0:
                {
                    offsetZ = -offset[0];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left1:
                {
                    offsetZ = -offset[1];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left2:
                {
                    offsetZ = -offset[2];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left3:
                {
                    offsetZ = -offset[3];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left4:
                {
                    offsetZ = -offset[4];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left5:
                {
                    offsetZ = -offset[5];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left6:
                {
                    offsetZ = -offset[6];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left7:
                {
                    offsetZ = -offset[7];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.Left8:
                {
                    offsetZ = -offset[8];
                    offsetY = 0;
                    break;
                }
            case NextShootDesitnation.RightDown0:
                {
                    offsetZ = offset[0];
                    offsetY = -offset[0];
                    break;
                }
            case NextShootDesitnation.RightDown1:
                {
                    offsetZ = offset[1];
                    offsetY = -offset[1];
                    break;
                }
            case NextShootDesitnation.RightDown2:
                {
                    offsetZ = offset[2];
                    offsetY = -offset[2];
                    break;
                }
            case NextShootDesitnation.RightDown3:
                {
                    offsetZ = offset[3];
                    offsetY = -offset[3];
                    break;
                }
            case NextShootDesitnation.RightDown4:
                {
                    offsetZ = offset[4];
                    offsetY = -offset[4];
                    break;
                }
            case NextShootDesitnation.RightDown5:
                {
                    offsetZ = offset[5];
                    offsetY = -offset[5];
                    break;
                }
            case NextShootDesitnation.RightDown6:
                {
                    offsetZ = offset[6];
                    offsetY = -offset[6];
                    break;
                }
            case NextShootDesitnation.RightDown7:
                {
                    offsetZ = offset[7];
                    offsetY = -offset[7];
                    break;
                }
            case NextShootDesitnation.RightDown8:
                {
                    offsetZ = offset[8];
                    offsetY = -offset[8];
                    break;
                }
            case NextShootDesitnation.RightUp0:
                {
                    offsetZ =  offset[0];
                    offsetY = offset[0];
                    break;
                }
            case NextShootDesitnation.RightUp1:
                {
                    offsetZ = offset[1];
                    offsetY = offset[1];
                    break;
                }
            case NextShootDesitnation.RightUp2:
                {
                    offsetZ = offset[2];
                    offsetY = offset[2];
                    break;
                }
            case NextShootDesitnation.RightUp3:
                {
                    offsetZ = offset[3];
                    offsetY = offset[3];
                    break;
                }
            case NextShootDesitnation.RightUp4:
                {
                    offsetZ = offset[4];
                    offsetY = offset[4];
                    break;
                }
            case NextShootDesitnation.RightUp5:
                {
                    offsetZ = offset[5];
                    offsetY = offset[5];
                    break;
                }
            case NextShootDesitnation.RightUp6:
                {
                    offsetZ = offset[6];
                    offsetY = offset[6];
                    break;
                }
            case NextShootDesitnation.RightUp7:
                {
                    offsetZ = offset[7];
                    offsetY = offset[7];
                    break;
                }
            case NextShootDesitnation.RightUp8:
                {
                    offsetZ = offset[8];
                    offsetY = offset[8];
                    break;
                }
            case NextShootDesitnation.LeftDown0:
                {
                    offsetZ = -offset[0];
                    offsetY = -offset[0];
                    break;
                }
            case NextShootDesitnation.LeftDown1:
                {
                    offsetZ = -offset[1];
                    offsetY = -offset[1];
                    break;
                }
            case NextShootDesitnation.LeftDown2:
                {
                    offsetZ = -offset[2];
                    offsetY = -offset[2];
                    break;
                }
            case NextShootDesitnation.LeftDown3:
                {
                    offsetZ = -offset[3];
                    offsetY = -offset[3];
                    break;
                }
            case NextShootDesitnation.LeftDown4:
                {
                    offsetZ = -offset[4];
                    offsetY = -offset[4];
                    break;
                }
            case NextShootDesitnation.LeftDown5:
                {
                    offsetZ = -offset[5];
                    offsetY = -offset[5];
                    break;
                }
            case NextShootDesitnation.LeftDown6:
                {
                    offsetZ = -offset[6];
                    offsetY = -offset[6];
                    break;
                }
            case NextShootDesitnation.LeftDown7:
                {
                    offsetZ = -offset[7];
                    offsetY = -offset[7];
                    break;
                }
            case NextShootDesitnation.LeftDown8:
                {
                    offsetZ = -offset[8];
                    offsetY = -offset[8];
                    break;
                }
            case NextShootDesitnation.LeftUp0:
                {
                    offsetZ = -offset[0];
                    offsetY = offset[0];
                    break;
                }
            case NextShootDesitnation.LeftUp1:
                {
                    offsetZ = -offset[1];
                    offsetY = offset[1];
                    break;
                }
            case NextShootDesitnation.LeftUp2:
                {
                    offsetZ = -offset[2];
                    offsetY = offset[2];
                    break;
                }
            case NextShootDesitnation.LeftUp3:
                {
                    offsetZ = -offset[3];
                    offsetY = offset[3];
                    break;
                }
            case NextShootDesitnation.LeftUp4:
                {
                    offsetZ = -offset[4];
                    offsetY = offset[4];
                    break;
                }
            case NextShootDesitnation.LeftUp5:
                {
                    offsetZ = -offset[5];
                    offsetY = offset[5];
                    break;
                }
            case NextShootDesitnation.LeftUp6:
                {
                    offsetZ = -offset[6];
                    offsetY = offset[6];
                    break;
                }
            case NextShootDesitnation.LeftUp7:
                {
                    offsetZ = -offset[7];
                    offsetY = offset[7];
                    break;
                }
            case NextShootDesitnation.LeftUp8:
                {
                    offsetZ = -offset[8];
                    offsetY = offset[8];
                    break;
                }
            case NextShootDesitnation.Center:
                {
                    offsetZ = 0;
                    offsetY = 0;
                    break;
                }
        }
    }
}

   
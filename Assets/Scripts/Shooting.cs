using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Shooting : NetworkBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Rigidbody projectile;
    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private Transform spawnPoint;

    private bool shooting = false;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0)))
        {
            shooting = true;
        }
    }

    void FixedUpdate()
    {
#if MOBILE_INPUT
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (SceneManager.GetActiveScene().rootCount == 2)
                {
                    CmdShootBullet();
                }
                else
                {
                   ShootBullet();
                }
            }
        }
#else
        if ((Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if (SceneManager.GetActiveScene().rootCount == 2)
            {
                CmdShootBullet();
            }
            else
            {
                ShootBullet();
            }
        }
#endif

    }
    [Command]
    private void CmdShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody instantiatedProjectile = bullet.GetComponent<Rigidbody>();
        instantiatedProjectile.gameObject.GetComponent<Bullet>().ownerName = gameObject.name;
        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        NetworkServer.Spawn(bullet);
        shooting = true;
    }

    private void ShootBullet()
    {
        Rigidbody instantiatedProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            instantiatedProjectile.gameObject.GetComponent<Bullet>().ownerName = gameObject.name;
        }


        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        shooting = true;
    }

}

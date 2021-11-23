using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float ShootForce;
    [SerializeField] private float TimeBetweenShoting, Spread, ReloadTime, TimeBetweenShots;
    private int BulletsShot, BulletLeft;
    [SerializeField] private int MagazienSize;
    [SerializeField] private bool AllowBottonHold;
    [SerializeField] private bool Shooting, ReadyToShoot, Reloading;
    [SerializeField] private Camera FpsCam;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private bool allowInvoke = true;

    private void Awake()
    {
        BulletLeft = MagazienSize;
        ReadyToShoot = true;
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (AllowBottonHold)
        {
            Shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            Shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (ReadyToShoot && Shooting && !Reloading && BulletLeft > 0)
        {
            BulletsShot = 0;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && BulletLeft <= MagazienSize && !Reloading)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        ReadyToShoot = false;
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 TargetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            TargetPoint = hit.point;

        }
        else
        {
            TargetPoint = ray.GetPoint(75);
        }

        Vector3 DirectionWithoutSpread = TargetPoint - shootingPoint.position;

        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);

        Vector3 DirectionWithSpread = DirectionWithoutSpread + new Vector3(x, y, Spread);

        GameObject currentBullet = Instantiate(Bullet, shootingPoint.position, Quaternion.identity);

        currentBullet.transform.forward = DirectionWithSpread.normalized;


        if (allowInvoke)
        {
            Invoke("ResetShot", TimeBetweenShoting);
            allowInvoke = false;
        }

        BulletLeft--;
        BulletsShot++;
    }

    private void ResetShot()
    {
        ReadyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        Reloading = true;
        Invoke("ReloadFinish", ReloadTime);
    }

    private void ReloadFinish()
    {
        BulletLeft = MagazienSize;
        Reloading = false;
    }
}

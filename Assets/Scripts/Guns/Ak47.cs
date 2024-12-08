using UnityEngine;
using System.Collections;

public class Ak47 : GunManager
{
    private bool _shooting;
    private bool _reloading;
    private void Awake()
    {
        MaxAmmo = AmoutAmmo;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) _shooting = true;
        if (Input.GetKeyDown(KeyCode.R)) _reloading = true;
    }
    public override void FixedUpdateNetwork()
    {
        if (_shooting)
        {
            _shooting = false;
            StartCoroutine(Shoot());
        }
        if (_reloading)
        {
            _reloading = false;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Shoot()
    {
        Debug.Log(3);
        if (AmoutAmmo > 0)
        {
            Debug.Log(4);
            Instantiate(Bullet, PointFire.transform.position, PointFire.transform.rotation);
            AmoutAmmo -= 1;
            yield return new WaitForSeconds(DelayShoot);
            if (Input.GetMouseButton(0))
                StartCoroutine(Shoot());
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(DelayReload);
        AmoutAmmo = MaxAmmo;
    }
}


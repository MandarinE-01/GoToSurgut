using Fusion;
using System.Collections;
using UnityEngine;

public class GunManager : NetworkBehaviour
{
    public GameObject Bullet;
    public GameObject PointFire;

    public int AmoutAmmo;
    public float DelayShoot;
    public float DelayReload;

    public int MaxAmmo;
}

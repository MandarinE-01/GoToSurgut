using Fusion;
using System.Collections;
using System.Data;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float TimeLifeBullet;
    [SerializeField] private float ForceBullet;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(TimerLife());
        
        _rb.AddForce(transform.rotation * Vector3.forward * ForceBullet);
    }
    
    IEnumerator TimerLife()
    {
        yield return new WaitForSeconds(TimeLifeBullet);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

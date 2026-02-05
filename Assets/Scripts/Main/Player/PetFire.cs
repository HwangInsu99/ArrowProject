using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFire : MonoBehaviour
{
    private float _damage;
    private float _speed = 5.0f;

    void Start()
    {
        transform.position -= new Vector3(0.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void SetDamage(float damage) => _damage = damage;

    public float Damage() => _damage;
}

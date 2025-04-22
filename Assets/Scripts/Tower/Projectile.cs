using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private int _damage;

    public Rigidbody Rigidbody => _rigidbody;
    public int Damage => _damage;

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
    }
}
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
    }
}
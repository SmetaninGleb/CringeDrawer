using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ExplosionAffected : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void AffectExplosion(Vector3 explosionPosition, float explosionForce)
    {
        Vector3 forceVector = transform.position - explosionPosition;
        forceVector = forceVector.normalized * explosionForce;
        _rigidbody.AddForce(forceVector, ForceMode2D.Impulse);
    }
}
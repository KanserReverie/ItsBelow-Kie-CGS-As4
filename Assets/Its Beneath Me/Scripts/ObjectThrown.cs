
using UnityEngine;

/// <summary>
/// This script is attached to any object thrown.
/// </summary>
public class ObjectThrown : MonoBehaviour
{
    [SerializeField] private float lifeTimer = 2.1f;
    [SerializeField] private Rigidbody thisRigidbody;

    private void Update()
    {
        lifeTimer -= Time.deltaTime;

        if(lifeTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.layer == LayerMask.NameToLayer("Work"))
        {
            Destroy(_collision.gameObject);
            Destroy(gameObject);
        }
    }
}
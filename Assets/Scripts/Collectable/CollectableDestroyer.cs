using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollectableDestroyer : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Collectable collectable))
        {
            collectable.gameObject.SetActive(false);
        }
    }
}
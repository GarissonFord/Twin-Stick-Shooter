using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    EnemyController ec;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = transform.right * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if((collider.gameObject.CompareTag("Enemy")))
        {
            //collider.gameObject.SendMessageUpwards("TakeDamage");
            ec = collider.gameObject.GetComponent<EnemyController>();
            ec.TakeDamage();
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Boundary"))
            Destroy(gameObject);
    }
}

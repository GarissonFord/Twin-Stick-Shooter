using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public GameObject target;
    public float moveSpeed;

    public float hitPoints;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        LookAtTarget();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
        ApproachTarget();
        UpdateColor();
    }

    void UpdateColor()
    {
        sr.color = new Color(hitPoints, 0.0f, 0.0f);
    }

    public void TakeDamage()
    {
        hitPoints -= 50.0f;
        UpdateColor();
    }

    void LookAtTarget()
    {
        Vector2 lookDirection = new Vector2(target.transform.position.x, target.transform.position.y);
        float rotationZ = Mathf.Atan2(lookDirection.y - transform.position.y, lookDirection.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    void ApproachTarget()
    {
        rb.velocity = transform.right * moveSpeed * Time.deltaTime;
    }
}

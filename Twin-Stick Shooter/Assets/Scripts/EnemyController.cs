using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public PlayerController target;
    public GameObject bloodSplatter;

    public float moveSpeed;

    public float hitPoints;

    public Color newColor;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        LookAtTarget();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
        ApproachTarget();
    }

    void UpdateColor()
    {
        newColor = new Color(hitPoints, 0.0f, 0.0f, 1.0f);
        sr.color = newColor;
    }

    public void TakeDamage()
    {
        hitPoints -= 0.2f;
        UpdateColor();
        if(hitPoints <= 0.0f)
        {
            Destroy(gameObject);
            Instantiate(bloodSplatter, transform.position, transform.rotation);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.SendMessageUpwards("TakeDamage");
    }
}

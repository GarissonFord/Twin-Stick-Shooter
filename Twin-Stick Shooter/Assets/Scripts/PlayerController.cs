using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public GameObject bulletPrefab, shooter;
    public GameObject bloodSplatter;

    public float hitPoints;
    public float moveSpeed;

    public AudioSource audio;
    public AudioClip fireAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * MOVEMENT
         */ 

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Gets the mouse position and converts the pixel value to a coordinate in the game world
        Vector2 lookDirection = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 finalLookDirection = Camera.main.ScreenToWorldPoint(lookDirection);

        //Arc Tangent to get the angle in radians that faces the mouse position
        float angleRadians = Mathf.Atan2(finalLookDirection.y - transform.position.y, finalLookDirection.x - transform.position.x);
        //Then converts to degrees for the sake of applying the rotation
        float angleDegrees = (180 / Mathf.PI) * angleRadians;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angleDegrees);

        rb.velocity = movement * moveSpeed * Time.deltaTime;

        /*
         * FIRING
         */
        if (Input.GetButtonDown("Fire1"))
            Fire();
    }

    void Fire()
    {
        audio.clip = fireAudio;
        audio.Play();
        Instantiate(bulletPrefab, shooter.transform.position, shooter.transform.rotation);
    }

    void UpdateColor()
    {
        Color newColor = new Color(0.0f, hitPoints, 0.0f, 1.0f);
        sr.color = newColor;
    }

    public void TakeDamage()
    {
        hitPoints -= 0.05f;
        UpdateColor();
        if (hitPoints <= 0.0f)
        {
            Instantiate(bloodSplatter, transform.position, transform.rotation);
            Destroy(gameObject);           
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    public GameController gc;

    public GameObject bulletPrefab, shooter;
    public GameObject bloodSplatter;

    public float hitPoints;
    public float maxHitPoints = 1.0f;
    public float moveSpeed;

    public AudioSource audio;
    public AudioClip fireAudio;

    public float timeOfLastShot, timeBetweenShots;

    public Image currentHealthBar;

    public float ratio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        gc = FindObjectOfType<GameController>();
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
        if (Input.GetButton("Fire1"))
            Fire();
    }

    void Fire()
    {
        if (Time.time - timeOfLastShot >= timeBetweenShots)
        {
            timeOfLastShot = Time.time;
            audio.clip = fireAudio;
            audio.Play();
            Instantiate(bulletPrefab, shooter.transform.position, shooter.transform.rotation);
        }
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
        UpdateHealthbar();
        if (hitPoints <= 0.0f)
        {
            gc.GameOver();
            Instantiate(bloodSplatter, transform.position, transform.rotation);
            GameObject temp = new GameObject();
            Instantiate(temp, transform.position, transform.rotation);
            temp.tag = "Player";
            Destroy(gameObject);           
        }
    }

    public void UpdateHealthbar()
    {
        ratio = hitPoints / maxHitPoints;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 0.25f, 1);
    }
}

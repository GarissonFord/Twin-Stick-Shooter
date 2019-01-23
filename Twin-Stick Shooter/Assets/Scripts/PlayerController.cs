using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject bulletPrefab, shooter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        rb.velocity = movement;

        /*
         * FIRING
         */
        if (Input.GetButtonDown("Fire1"))
            Fire();
    }

    void Fire()
    {
        Instantiate(bulletPrefab, shooter.transform.position, shooter.transform.rotation);
    }
}

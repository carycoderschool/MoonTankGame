using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShellMover : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public bool canHurt;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            if (canHurt)
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
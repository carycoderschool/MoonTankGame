using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    private Rigidbody rb;
    private float moveVertical;
    public float speed;
    private float rotation;
    public float rotationSpeed;
    public GameObject tankBase;
    public GameObject turret;
    public float turretRotation;
    public GameObject shell;
    public GameObject shellSpawner;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVertical = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
        tankBase.transform.RotateAround(transform.position, Vector3.up, rotation * rotationSpeed);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        turret.transform.LookAt(new Vector3(mousePos.x, turret.transform.position.y, mousePos.z));

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate<GameObject>(shell, shellSpawner.transform.position, turret.transform.rotation);
        }



    }

    void FixedUpdate()
    {
        rb.velocity = tankBase.transform.TransformDirection(new Vector3(0.0f , 0.0f, moveVertical * speed));
    }
}

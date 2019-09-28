using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public GameObject tank;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tank != null)
        {
            transform.position = new Vector3(tank.transform.position.x, 10, tank.transform.position.z);
        }
    }
}

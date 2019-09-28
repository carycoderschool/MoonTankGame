using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMover : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject playerTank;
    public GameObject shell;
    public GameObject shellSpawner;
    public GameObject turret;
    public float chaseRange;
    public float fireRange;
    public float delayBetweenShots;
    private float fireTimer;
    private float playerDistance;
    public float spread;
    private float fireSpread;
    private Rigidbody rb;
    public float numberOfShells;
    public bool selfDestruct;
    public EnemyTankSpawner factory;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Test 1");
        if (playerTank != null)
        {
            Debug.Log("Test 2");
            playerDistance = Vector3.Distance(playerTank.transform.position, transform.position);
            SelectState();

            if (canShoot())
            {
                Debug.Log(gameObject.name);
                turret.transform.LookAt(new Vector3(playerTank.transform.position.x, turret.transform.position.y, playerTank.transform.position.z));
            }
        }
    }

    private void OnDestroy()
    {
        if (factory != null)
        {
            factory.numberOfTanks -= 1;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < numberOfShells; i++)
        {
            fireSpread = Random.Range(-spread, spread);
            turret.transform.Rotate(turret.transform.rotation.x, fireSpread, turret.transform.rotation.z);
            Instantiate<GameObject>(shell, shellSpawner.transform.position, turret.transform.rotation);
            if (selfDestruct)
            {
                Destroy(gameObject);
            }
        }
    }

    void Chase()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(playerTank.transform.position);
        }
    }

    void Patrol()
    {

    }

    void SelectState()
    {
        Debug.Log("Test 3");
        if (playerDistance <= fireRange && canShoot())
        {
            fireTimer += Time.deltaTime;
            Debug.Log("Test 4");

            if (fireTimer == 0)
            {
                Shoot();
            }
            else if (fireTimer >= delayBetweenShots)
            {
                Shoot();
                fireTimer = 0;
            }
        }
        else if (playerDistance <= chaseRange && playerDistance > fireRange)
        {
            Chase();
            fireTimer = 0;
        }
    }

    bool canShoot()
    {
        //Not working with prefabs
        Debug.Log("Test 5");
        RaycastHit hit;
        Vector3 direction = playerTank.transform.position - transform.position;

        if (Physics.Raycast(turret.transform.position, direction, out hit))
        {
            Debug.Log("Test 6");
            //Debug.DrawLine(playerTank.transform.position, transform.position);
            //return hit.transform == playerTank.transform;
            //Debug.Log(hit.transform == playerTank.transform);
            return true;
        }
        Debug.Log(Physics.Raycast(turret.transform.position, direction, out hit));
        return false;
    }
}

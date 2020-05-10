using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShootingTrap : MonoBehaviour
{
    public Transform shootingPoint;

    [SerializeField]
    public GameObject arrowPrefab;

    [SerializeField]
    public float arrowSpeed;

    public float rotationDegree;

    private void Start()
    {
        transform.eulerAngles = new Vector3(0.0f, rotationDegree, 0.0f);

        InvokeRepeating("Shoot", 0f, 2.0f);
    }

    private void Shoot()
    {
        GameObject arrowClone = Instantiate(arrowPrefab, shootingPoint.position, shootingPoint.transform.rotation);
        arrowClone.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * arrowSpeed;
    }
}

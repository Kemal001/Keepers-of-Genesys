using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //public Transform player;

    private Vector3 target;
    private Vector3 difference;

    private float rotationZ;
    private float nextFire;

    public float projectileSpeed;
    public float fireRate;

    [Header("Camera")]
    private Camera cam;
    
    [Header("Prefabs")]
    public GameObject projectilePrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Weapon")]
    public GameObject crosshair;
    public GameObject gun;
    public GameObject gunFirePoint;
    public GameObject flashPoint;

    private void Start()
    {
        Cursor.visible = false;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        target = cam.transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(target.x, target.y);

        //var playerScreenPoint = cam.transform.GetComponent<Camera>().WorldToScreenPoint(player.transform.position);

        //if (target.x < playerScreenPoint.x)
        //{ // mouse is on right side of player
        //    gun.transform.localScale = new Vector3(-1, 1, 1); // or activate look right some other way
        //}
        //else if (target.x > playerScreenPoint.x)
        //{ // mouse is on left side
        //    gun.transform.localScale = new Vector3(1, 1, 1); // activate looking left
        //}

        GunRotation();

        if(Input.GetMouseButton(0) && Time.time > nextFire)
        {
            Instantiate(muzzleFlashPrefab, flashPoint.transform.position, flashPoint.transform.rotation);
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            Shoot(direction, rotationZ);
            nextFire = Time.time + fireRate;
        }
    }

    private void Shoot(Vector2 direction, float rotationZ)
    {
        AudioManager.instance.Play("Shot");

        GameObject projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = gunFirePoint.transform.position;
        projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        Destroy(projectile, 1f);
    }

    private void GunRotation()
    {
        difference = target - gun.transform.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}

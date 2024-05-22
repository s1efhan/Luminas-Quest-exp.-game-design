using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingscript2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject cannonBall;
    public Transform barrel;

    public float force;
    public float rotationSpeed = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject bullet = Instantiate(cannonBall, barrel.position, barrel.rotation);
            bullet.GetComponent<Rigidbody>().velocity = barrel.forward * force * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Y))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.U))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
}

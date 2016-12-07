using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireAtTarget : MonoBehaviour
{
    private GameObject objectsToTrigger;
    public Camera cam1; 
    private Ray ray;
    private RaycastHit hit;
    private bool objectHit;
    private Transform cam;
    public float forceIntensity = 1750f;

    private GameObject ammo;

    public void Start()
    {
        objectsToTrigger = GameObject.FindGameObjectWithTag("Target");
        ammo = GameObject.FindGameObjectWithTag("Ammo");
        print("CLICKING!");
    }
    void Update()
    {
        
    }
    public void OnPointerClick()
    {
        GameObject bulletCopy = (GameObject)Instantiate(ammo, ammo.transform.position, Quaternion.identity);
        bulletCopy.GetComponent<Collider>().enabled = true;
        bulletCopy.GetComponent<Rigidbody>().useGravity = true;
        //bulletCopy.GetComponent<Shader>() = Color.black;
        Vector3 dir = ammo.transform.position - new Vector3(objectsToTrigger.transform.position.x, objectsToTrigger.transform.position.y, objectsToTrigger.transform.position.z);
        dir.Normalize();
        bulletCopy.GetComponent<Rigidbody>().AddForce(-dir * forceIntensity);
        print("CLICKING!");
    }



}

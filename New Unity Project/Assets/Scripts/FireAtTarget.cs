using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireAtTarget : MonoBehaviour
{
    private GameObject[] objectsToTrigger;
    private Ray ray;
    private RaycastHit hit;
    private bool objectHit;
    private Transform cam;
    public float forceIntensity = 1;
    public GameObject CenterPlatform;
    public GameObject ObjectToFire;

    void Start()
    {
        objectsToTrigger = GameObject.FindGameObjectsWithTag("Target");
        cam = GameObject.Find("Camera").transform;
        objectHit = false;
    }
    void Update()
    {
        ray = new Ray(cam.position, cam.forward);
        if (Physics.Raycast(ray, out hit))
        {
            objectHit = false;
            //print("Name Obje: " + hit.collider.gameObject.name);
            foreach (GameObject g in objectsToTrigger)
            {
                if (g.name == hit.collider.gameObject.name)
                {
                    objectHit = true;
                }
            }
            if (objectHit)
            {
                ObjectToFire.layer = 0;
                ObjectToFire.transform.localPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            else
                ObjectToFire.layer = 8;
        }
    }
    public void OnPointerClick()
    {
        GameObject bulletCopy = (GameObject)Instantiate(ObjectToFire, ObjectToFire.transform.position, Quaternion.identity);
        bulletCopy.GetComponent<Collider>().enabled = true;
        bulletCopy.GetComponent<Rigidbody>().useGravity = true;
        //bulletCopy.GetComponent<Shader>() = Color.black;
        Vector3 dir = ObjectToFire.transform.position - new Vector3(CenterPlatform.transform.position.x, CenterPlatform.transform.position.y + 2, CenterPlatform.transform.position.z);
        dir.Normalize();
        bulletCopy.GetComponent<Rigidbody>().AddForce(-dir * forceIntensity);
        //print("CLICKING!");
    }



}

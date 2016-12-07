using UnityEngine;
using System.Collections;

public class RotateRemote : MonoBehaviour {

    // Use this for initialization
    public int xCoord, yCoord, zCoord = 0;
    public bool isRandom = false;
    public bool isRotate = true;
    void Start ()
    {

        
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isRandom)
        {
            xCoord = Random.Range(0, 2);
            yCoord = Random.Range(0, 2);
            zCoord = Random.Range(0, 2);
        }
        if (isRotate)
            this.transform.Rotate(new Vector3(xCoord, yCoord, zCoord), 1.0f);
    }
}

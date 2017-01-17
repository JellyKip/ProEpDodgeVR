using UnityEngine;
using System.Collections;

public class RemoveShitFromCardboard : MonoBehaviour {

	// Use this for initialization
    public void RemoveMyShit(GameObject[] arr)
    {
        foreach (GameObject item in arr)
        {
            Destroy(item);
        }
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

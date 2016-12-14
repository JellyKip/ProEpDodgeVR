using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public int _id;
    public Transform _bulletSpawn;
    public GameObject _bulletPrefab;

    public void SpawnBullet()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3[] tempList = player.GetComponent<SteamVR_PlayArea>().cornerPositions;

        Vector3 temp = GetCorner(tempList);

        GameObject bulletCopy = (GameObject)Instantiate(_bulletPrefab, temp, Quaternion.identity);

        Destroy(bulletCopy, 3.0f);
    }

    Vector3 GetCorner(Vector3[] vectors)
    {
        switch (_id)
        {
            case 1:
                return vectors[0];
            case 2:
                return vectors[1];
            case 3:
                return vectors[2];
            case 4:
                return vectors[3];
            case 5:
                return vectors[0] + (vectors[1] - vectors[0])/2;
            case 6:
                return vectors[1] + (vectors[2] - vectors[1]) / 2;
            case 7:
                return vectors[2] + (vectors[3] - vectors[2]) / 2;
            case 8:
                return vectors[3] + (vectors[0] - vectors[3]) / 2;
            default:
                return new Vector3();
        }
    }
}

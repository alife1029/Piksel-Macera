using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;

    public void GeneratePlatform (Vector3 playerPos) {
        /*Vector3 newTransform = new Vector3(
            playerPos.x + Random.Range(-2f, 2f),
            playerPos.y +
        )*/
        GameObject newPlatform = Instantiate(platform, new Vector3(4, 4, 4), Quaternion.Euler(Vector3.zero));
    }
}

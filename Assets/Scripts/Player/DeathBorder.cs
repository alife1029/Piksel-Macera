using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBorder : MonoBehaviour
{
    private void OnCollisionEnter2D (Collision2D collision) {
        if(collision.gameObject.CompareTag("Death Border")) {
            PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
            playerData.starting = "Load Checkpoint";

            SaveSystem.Save(playerData, SaveSystem.playerDataPath);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

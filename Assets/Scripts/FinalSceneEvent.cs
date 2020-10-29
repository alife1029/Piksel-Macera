using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneEvent : MonoBehaviour
{
    public void goMainMenu () {
        
        SceneManager.LoadScene(0);
    }
}

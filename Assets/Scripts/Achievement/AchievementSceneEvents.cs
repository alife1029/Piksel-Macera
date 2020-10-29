using UnityEngine;
using UnityEngine.SceneManagement;

namespace Achievement
{
    public class AchievementSceneEvents : MonoBehaviour
    {
        public void btnBack_Click() {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

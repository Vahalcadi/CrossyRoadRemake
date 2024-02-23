using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] AudioListener audioListener;

    public void VolumeOff()
    {
        if (audioListener != null)
            audioListener.enabled = false;
        else
            Debug.Log("AudioListener not found");
    }
    public void VolumeOn()
    {
        if (audioListener != null)
            audioListener.enabled = true;
        else
            Debug.Log("AudioListener not found");
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(0);
    }
}

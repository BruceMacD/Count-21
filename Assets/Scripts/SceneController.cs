using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public AudioSource click;

    public void SceneLoad(string scene)
    {
        PlaySound();
        SceneManager.LoadScene(scene);
    }

    public void PlaySound()
    {
        click.Play();
    }


}

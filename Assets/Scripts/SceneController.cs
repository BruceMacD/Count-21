using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//logic for changing scences on button press

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

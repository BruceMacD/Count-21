using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour {

    public void SceneLoad(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("main");
    }
}
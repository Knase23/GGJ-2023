using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterTime : MonoBehaviour
{

    public string SceneToLoad;
    // Start is called before the first frame update
    public void Trigger(float time)
    {
        StartCoroutine(LoadAfterTime(time));
    }

    IEnumerator LoadAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneToLoad);
    }
}

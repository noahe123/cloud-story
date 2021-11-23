using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    AudioSource sceneAudio;

    bool fadeVolume = false;

    private void Start()
    {
        sceneAudio = GameObject.Find("Scene Audio").GetComponent<AudioSource>();
    }

    public IEnumerator LoadLevel(string levelName, float transitionTime)
    {
        transition.SetTrigger("Start");
        fadeVolume = true;


        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);

    }

    private void FixedUpdate()
    {
        if (fadeVolume)
        {
            sceneAudio.volume -= .02f;

        }
    }
}

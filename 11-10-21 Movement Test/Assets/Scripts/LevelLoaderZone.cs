using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoaderZone : MonoBehaviour
{
    GameObject levelLoader;
    public string myLevelName;

    private void Start()
    {
        levelLoader = GameObject.Find("LevelLoader");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(levelLoader.GetComponent<LevelLoader>().LoadLevel(myLevelName, 1));
        }
    }
}

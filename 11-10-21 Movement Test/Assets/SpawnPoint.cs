using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnPoint : MonoBehaviour
{
    GameObject player;

    CinemachineFreeLook myFreeLook;

    // Start is called before the first frame update
    void Awake()
    {
        myFreeLook = GameObject.Find("Third Person Camera").GetComponent<CinemachineFreeLook>();
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
        player.transform.rotation = Quaternion.identity;
        player.transform.Rotate(0, 180, 0);
        player.GetComponent<SAudioManager>().Stop("sand");
        player.GetComponent<SAudioManager>().Stop("grass");
        player.GetComponent<SAudioManager>().Stop("wood");


        myFreeLook.Follow = player.transform;
        myFreeLook.LookAt = player.transform;

    }

}

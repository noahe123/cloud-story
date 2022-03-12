using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridParticlesSpawner : MonoBehaviour
{
    Vector3 planeStartPos;
    GameObject gameplayObj;
    public GameObject gridObj;
    int numGridsSpawned = 1;
    Vector3 childSpawnPositionOffset;

    // Start is called before the first frame update
    void Start()
    {
        childSpawnPositionOffset = transform.GetChild(1).transform.position;
        gameplayObj = GameObject.Find("Gameplay");
        planeStartPos = gameplayObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float planeZPos = gameplayObj.transform.position.z;
        float planeZStartPos = planeStartPos.z;
        int gridSpawnOffset = 80 * numGridsSpawned;
        if (planeZPos > planeZStartPos + gridSpawnOffset)
        {
            Instantiate(gridObj, childSpawnPositionOffset + new Vector3(0, 0, gridSpawnOffset), Quaternion.identity);
            numGridsSpawned++;
        }
    }
}
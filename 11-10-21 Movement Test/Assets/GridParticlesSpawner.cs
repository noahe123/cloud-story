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
    float scaleFactor;

    // Start is called before the first frame update
    void Start()
    {
        scaleFactor = transform.localScale.x;
        childSpawnPositionOffset = transform.GetChild(2).transform.position;
        gameplayObj = GameObject.Find("Gameplay");
        planeStartPos = gameplayObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float planeZPos = gameplayObj.transform.position.z;
        float planeZStartPos = planeStartPos.z;
        float gridSpawnOffset = 80 * scaleFactor * numGridsSpawned;
        if (planeZPos > planeZStartPos + gridSpawnOffset)
        {
            
            GameObject spawnedGrid = Instantiate(gridObj, Vector3.zero, Quaternion.identity);
            spawnedGrid.transform.parent = transform;
            spawnedGrid.transform.localScale = Vector3.one;
            spawnedGrid.transform.position = childSpawnPositionOffset + new Vector3(0, 0, gridSpawnOffset);
            numGridsSpawned++;
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
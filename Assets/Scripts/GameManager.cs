using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> gameObjects = new();
    public List<TerrainSpawner> terrains = new();

    // Start is called before the first frame update
    void Start()
    {
        gameObjects.Add(Instantiate(terrains[0].terrain));
        gameObjects[^1].GetComponent<TerrainSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

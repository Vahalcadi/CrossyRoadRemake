using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector3 currentPos = new Vector3(0, 0, 0);

    public List<GameObject> gameObjects = new();
    public List<TerrainScriptableObj> terrains = new();

    // Start is called before the first frame update
    void Start()
    {
        gameObjects.Add(Instantiate(terrains[0].terrain));
        gameObjects[^1].GetComponent<TerrainScriptableObj>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

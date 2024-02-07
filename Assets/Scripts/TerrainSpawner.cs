using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainSpawnerSO", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class TerrainSpawner : ScriptableObject
{
    public GameObject terrain;
    public bool dieOnWater;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainScriptableObj", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class TerrainScriptableObj : ScriptableObject
{
    public GameObject terrain;
    public bool dieOnWater;
}

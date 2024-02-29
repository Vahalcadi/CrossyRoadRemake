using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;
    private List<int> extractedSpawnpoints = new();
    private int randomPosition;
    [SerializeField] private int minNumberOfObstacles;
    [SerializeField] private int maxNumberOfObstacles;

    // Start is called before the first frame update
    void Start()
    {
        int randomNumberOfObstacles = Random.Range(minNumberOfObstacles, maxNumberOfObstacles + 1);

        for (int i = 0; i < randomNumberOfObstacles; i++)
            GenerateObstacle();
    }

    private void GenerateObstacle()
    {
        CheckExtractedPosition();
        GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
        Vector3 position = new Vector3(randomPosition, transform.position.y + obstacle.transform.position.y,transform.position.z);
        Instantiate(obstacle, position, obstacle.transform.rotation);
    }

    private void CheckExtractedPosition()
    {
        randomPosition = Random.Range(-4, 5);

        if(extractedSpawnpoints.Contains(randomPosition))
            CheckExtractedPosition();
        else
            extractedSpawnpoints.Add(randomPosition);
    }
}

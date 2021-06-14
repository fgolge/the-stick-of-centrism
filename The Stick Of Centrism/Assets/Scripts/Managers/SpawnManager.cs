using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject stickPrefab;
    public GameObject ballPrefab;
    public GameObject holePrefab;

    List<GameObject> spawnedHoles = new List<GameObject>();

    List<Vector3> holeSpawnPoints = new List<Vector3>();

    private Vector3 ballStartPos = new Vector3(0f, 0.165f, 0f);
    private Vector3 stickStartPosition = new Vector3(0f, 0.129f, 0f);
    

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(OnStateChangeHandler);
        Spawn();
    }

    private void OnStateChangeHandler(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (currentState == GameManager.GameState.RUNNING && previousState == GameManager.GameState.PREGAME)
        {
            Spawn();
        }
        if (currentState == GameManager.GameState.END && previousState == GameManager.GameState.RUNNING)
        {
            GameOver();
        }
    }
    
    private void Spawn()
    {
        Instantiate(stickPrefab, stickStartPosition, stickPrefab.transform.rotation);
        Instantiate(ballPrefab, ballStartPos, ballPrefab.transform.rotation);

        for (int i = 0; i < 50; i++)
        {
            Vector3 RandomPoint = new Vector3(Random.Range(-0.500f, 0.500f), Random.Range(0.500f, 1.800f), 0.039f);
            bool didOverlap = false;

            foreach (Vector3 point in holeSpawnPoints)
            {
                if (Vector3.Distance(point, RandomPoint) < 0.1f)
                {
                    didOverlap = true;
                }
            }
            if (didOverlap == false)
            {
                holeSpawnPoints.Add(RandomPoint);
            }
            else
            {
                i--;
            }
        }

        //Debug.Log(holeSpawnPoints.Count);
        /*for (int i = 0; i < holeSpawnPoints.Count; i++)
        {
            spawnedHoles[i] = Instantiate(holePrefab, holeSpawnPoints[i], holePrefab.transform.rotation);
        }*/

        foreach(Vector3 point in holeSpawnPoints)
        {
            spawnedHoles.Add(Instantiate(holePrefab, point, holePrefab.transform.rotation));
        }
    }

    private void GameOver()
    {
        spawnedHoles.Clear();
    }
}

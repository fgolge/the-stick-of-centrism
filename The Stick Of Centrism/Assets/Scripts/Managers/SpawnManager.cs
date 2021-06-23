using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject stickPrefab;
    public GameObject ballPrefab;
    public GameObject blackHolePrefab;
    public GameObject redHolePrefab;

    [SerializeField] private List<GameObject> spawnedHoles;

    private GameObject spawnedStick;
    private GameObject spawnedBall;

    private MoveStick _moveStick;

    List<Vector3> holeSpawnPoints = new List<Vector3>();

    private Vector3 ballStartPos = new Vector3(0f, 0.165f, 0f);
    private Vector3 stickStartPosition = new Vector3(0f, 0.129f, 0f);

    private int holeNumber = 50;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(OnStateChangeHandler);

        // Creating stick and ball
        spawnedStick = Instantiate(stickPrefab);
        spawnedBall = Instantiate(ballPrefab);

        _moveStick = spawnedStick.GetComponent<MoveStick>();
        
        // Creating Hole Object Pooler
        spawnedHoles = new List<GameObject>();

        spawnedHoles.Add(Instantiate(redHolePrefab));

        for(int i=0; i < holeNumber; i++)
        {
            spawnedHoles.Add(Instantiate(blackHolePrefab));
        }

        OnStart();
    }

    private void OnStateChangeHandler(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (currentState == GameManager.GameState.RUNNING && (previousState == GameManager.GameState.PREGAME || previousState == GameManager.GameState.END)) 
        {
            OnStart();
        }
        if (currentState == GameManager.GameState.END && previousState == GameManager.GameState.RUNNING)
        {
            OnGameOver();
        }
    }
    
    public void FreezeStick()
    {
        _moveStick.enabled = false;
    }

    private void OnStart()
    {
        // Setting ball and stick active and their positions
        spawnedStick.transform.position = stickStartPosition;
        spawnedStick.transform.rotation = stickPrefab.transform.rotation;
        spawnedStick.gameObject.SetActive(true);
        _moveStick.enabled = true;
        spawnedBall.transform.position = ballStartPos;
        spawnedBall.transform.rotation = ballPrefab.transform.rotation;
        spawnedBall.gameObject.SetActive(true);

        // Random spawn point of red hole
        Vector3 RandomPoint = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(1.4f, 1.7f), 0.039f);
        holeSpawnPoints.Add(RandomPoint);
        
        // Generating random spawn points for holes
        for (int i = 0; i < holeNumber; i++)
        {
            RandomPoint = new Vector3(Random.Range(-0.500f, 0.500f), Random.Range(0.500f, 1.800f), 0.039f);
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

        // Setting holes active and their positions
        for(int i=0; i < spawnedHoles.Count; i++)
        {
            spawnedHoles[i].transform.position = holeSpawnPoints[i];
            spawnedHoles[i].transform.rotation = blackHolePrefab.transform.rotation;
            spawnedHoles[i].gameObject.SetActive(true);
        }

        // Clearing random point list to generate new points next game
        holeSpawnPoints.Clear();
    }

    private void OnGameOver()
    {
        foreach(GameObject hole in spawnedHoles)
        {
            hole.gameObject.SetActive(false);
        }
        spawnedStick.gameObject.SetActive(false);
        spawnedBall.gameObject.SetActive(false);
    }
}

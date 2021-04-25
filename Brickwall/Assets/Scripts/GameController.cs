using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController Instance;
    public Text scoreText;

    public int score = 0;

    public float scrollSpeed = -2f;
    public float spawnRate = 5f;

    public int wallPoolSize = 5;
    public GameObject wallPrefab;

    private GameObject[] wallPool;
    private float timeSinceLastSpawn = 0f;
    private int currentWall = 0;

    private float spawnYPos = 8f;
    private float spawnXPosMin = -42f;
    private float spawnXPosMax = -28f;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        wallPool = new GameObject[wallPoolSize];

        for (int i = 0; i < wallPoolSize; ++i) wallPool[i] = Instantiate(wallPrefab, new Vector2(-70f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= spawnRate)
        {
            wallPool[currentWall++].transform.position = new Vector2(Random.Range(spawnXPosMin, spawnXPosMax), spawnYPos);

            if (currentWall == wallPoolSize) currentWall = 0;

            timeSinceLastSpawn = 0;
        }
    }

    public void Score()
    {
        scoreText.text = "Score: " + (++score);
    }

    public void GameOver()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

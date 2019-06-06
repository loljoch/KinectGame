using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateGrid : MonoBehaviour
{
    public Transform topLeft, bottomRight;
    public Square sqrScript;
    public GameObject squarePrefab;
    public List<GameObject> spawnedSquares;
    public float increment = 0.11f;

    public Color scorePoint;
    public float timeBetweenScorePointSpawn, timeBetweenPenaltySpawn;
    public TextMeshProUGUI score;



    // Start is called before the first frame update
    void Start()
    {
        sqrScript = topLeft.GetComponent<Square>();
        scorePoint = sqrScript.scorePointColor;

        spawnedSquares = new List<GameObject>();
        float xDifference = Mathf.RoundToInt((topLeft.position.x - bottomRight.position.x) / increment);
        float yDifference = Mathf.RoundToInt((topLeft.position.y - bottomRight.position.y) / increment);
        xDifference = (xDifference > 0) ? xDifference : xDifference * -1;
        yDifference = (yDifference > 0) ? yDifference : yDifference * -1;
        spawnedSquares.Add(topLeft.gameObject);
        Destroy(bottomRight.gameObject);

        for (int w = -1; w < yDifference; w++)
        {
            for (int i = 0; i < xDifference; i++)
            {
                Vector3 lastXSquare = spawnedSquares[spawnedSquares.Count - 1].transform.position;
                Vector3 nextXPosition = new Vector3(lastXSquare.x + increment, lastXSquare.y, lastXSquare.z);
                spawnedSquares.Add(Instantiate(squarePrefab, nextXPosition, topLeft.rotation, transform));
            }
            Vector3 lastYSquare = spawnedSquares[spawnedSquares.Count - 1].transform.position;
            Vector3 nextYPosition = new Vector3(topLeft.position.x, lastYSquare.y - increment, lastYSquare.z);
            spawnedSquares.Add(Instantiate(squarePrefab, nextYPosition, topLeft.rotation, transform));

        }
        Destroy(spawnedSquares[spawnedSquares.Count - 1]);

        for (int i = 0; i < spawnedSquares.Count; i++)
        {
            spawnedSquares[i].GetComponent<Square>().score = score;
        }

        StartCoroutine(RandomScorePoint());
        StartCoroutine(RandomPenatly());
    }


    IEnumerator RandomScorePoint()
    {
        int square = Random.Range(0, spawnedSquares.Count);
        Square squareScript = spawnedSquares[square].GetComponent<Square>();
        while (squareScript.isPlayer || squareScript.isScorePoint || squareScript.isPenalty)
        {
            square = Random.Range(0, spawnedSquares.Count);
            squareScript = spawnedSquares[square].GetComponent<Square>();
        }

        squareScript.SetToScorePoint();
        Debug.Log("Coroutine randomscorepoint");
        yield return new WaitForSeconds(timeBetweenScorePointSpawn);
        StartCoroutine(RandomScorePoint());
    }

    IEnumerator RandomPenatly()
    {
        int square = Random.Range(0, spawnedSquares.Count);
        Square squareScript = spawnedSquares[square].GetComponent<Square>();
        while (squareScript.isPlayer || squareScript.isScorePoint || squareScript.isPenalty)
        {
            square = Random.Range(0, spawnedSquares.Count);
            squareScript = spawnedSquares[square].GetComponent<Square>();
        }

        squareScript.SetToPenalty();

        yield return new WaitForSeconds(timeBetweenPenaltySpawn);
        StartCoroutine(RandomPenatly());
    }

}

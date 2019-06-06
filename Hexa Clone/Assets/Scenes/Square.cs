using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Square : MonoBehaviour
{
    private new Renderer renderer;
    public Color playerColor, otherColor, scorePointColor, penaltyColor;
    public TextMeshProUGUI score;
    public bool isPlayer, isScorePoint, isPenalty;
    public float penaltyWaitTime;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        otherColor = renderer.material.color;

    }

    //Makes sure the player cubes stay the right color
    private void OnTriggerStay(Collider other)
    {
        if (renderer.material.color != playerColor)
        {
            if (other.CompareTag("Player"))
            {
                renderer.material.color = playerColor;
                isPlayer = true;
            }
        }
    }

    //Sets the player cubes
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isScorePoint)
            {
                Debug.Log("You touched a scorePoint");
                ScorePoint(1);
                isScorePoint = false;
            } else if (isPenalty)
            {
                ScorePoint(-1);
                isPenalty = false;
            }
            renderer.material.color = playerColor;
            isPlayer = true;

        }
    }

    //Sets the cube back to original color
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            renderer.material.color = otherColor;
            isPlayer = false;
        }
    }

    void ScorePoint(int number)
    {
        Debug.Log("Points increased");
        score.SetText((int.Parse(score.text) + number).ToString());
    }

    public void SetToScorePoint()
    {
        Debug.Log("SCOREPOINT SET");
        GetComponent<Renderer>().material.color = scorePointColor;
        isScorePoint = true;
    }

    public void SetToPenalty()
    {
        GetComponent<Renderer>().material.color = penaltyColor;
        isPenalty = true;
        StartCoroutine(PenaltyTimer());
    }

    IEnumerator PenaltyTimer()
    {
        yield return new WaitForSeconds(penaltyWaitTime);
        renderer.material.color = otherColor;
        isPenalty = false;
    }
}

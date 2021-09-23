using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score = 0,turns = 0;
    public static bool stop;
    public Text text1;
    public Text text2;
    private void Update()
    {
        //Keep track of score and turns
        text1.text = Score.ToString();
        if(turns == 1 && Score == 10)
        {
            text2.text = "STRIKE!!!";
            stop = true;
        }
        else if(turns == 2 && Score == 10)
        {
            text2.text = "SPARE!!";
            stop = true;
        }
        else if(turns>=2 && Score != 10)
        {
            text2.text = "You Lose";
            stop = true;
        }
    }
}

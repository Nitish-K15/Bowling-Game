using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour
{
    void Update()
    {
        if (transform.up.y < 0.6f) // checks to see if pin is facing anywhere other than up
        {
            ScoreManager.Score += 1;
            this.enabled = false;
        }
    }
}

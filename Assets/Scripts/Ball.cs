using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float min, max,minRotation,maxRotation; //clamping values for movement and rotation
    public bool inPlay,playing = true; //bools for flow control
    Rigidbody rb;
    float yRotation = 0f;
    public float throwForce; //throwingforce of the ball
    private Vector3 Startpos;
    private void Start()
    {
        Startpos = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!inPlay)  //if ball is not in play player can move it otherwise can't
        {
            playing = true;
            yRotation = Mathf.Clamp(yRotation, minRotation, maxRotation);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            float x = Input.GetAxis("Horizontal") * 20 * Time.deltaTime;
            transform.Translate(new Vector3(x, 0, 0));
            Vector3 Pos = gameObject.transform.position;
            Pos.x = Mathf.Clamp(Pos.x, min, max);
            gameObject.transform.position = Pos;
            if (Input.GetKey(KeyCode.E))
            {
                yRotation += 1;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                yRotation -= 1;
            }
        }
        if(playing && rb.velocity==Vector3.zero && inPlay && !ScoreManager.stop) // reset ball position if it stops midway on the path
        {
            playing = false;
            gameObject.transform.position = Startpos;
            rb.velocity = Vector3.zero;
            inPlay = false;
            ScoreManager.turns += 1; 
        }
    }

    IEnumerator BallReset() //coroutine to reset ball after pin collision
    {
        playing = false;
        yield return new WaitForSeconds(5f);
        gameObject.transform.position = Startpos;
        rb.velocity = Vector3.zero;
        inPlay = false;
        yield return null;
    }

    private void FixedUpdate()
    {
        if (!inPlay && !ScoreManager.stop)
        {
            if (Input.GetKey(KeyCode.Space)) //add force and increment turn
            {
                rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                inPlay = true;
                ScoreManager.turns += 1;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pin") && playing)
        {
            StartCoroutine(BallReset());
        }
    }
}



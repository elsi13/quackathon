using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DuckMove : MonoBehaviour
{
    public int radius;
    public float speed;
    System.Random rand;
    private int[] numbers = new int[2] { -1, 1 };
    private float period = 0.3f;
    private float nextActionTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random((int)DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {
        var moveHorizontal = rand.Next(2);
        Vector3 movement = new Vector3(numbers[moveHorizontal], 0.0f, 0.0f);

        if ((transform.position + movement).x < (radius) &&
            (transform.position + movement).x > (-radius))
        {
            transform.Translate(movement * Time.deltaTime * speed);
        }
    }

}

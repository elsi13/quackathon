using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TakeDown : MonoBehaviour
{
    GameManager gameManager;
    System.Random rand;
    private int[] numbers = new int[2] { 0, 1 };

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rand = new System.Random((int)DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
        {
            var point = rand.Next(2);
            gameManager.UpdateScore(numbers[point]);
            Destroy(gameObject);
        }
    }
}

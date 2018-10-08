﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CharacterController : MonoBehaviour {

    /**
     * Player index ID, don't change after the object has started. From 0-3 (0=player 1, 3=player 4)
     */
    public int PlayerID;
    bool playerIndexSet = false;
    public bool IsPlayerPlaying;
    PlayerIndex playerIndex;
    public float runSpeed;
    public float jumpForce;

    GamePadState state;
    GamePadState prevState;

    public bool isDead = false;

    bool leftPressed = false;
    bool rightPressed = false;
    bool jumpPressed = false;

    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                //PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(playerIndex);
                if (testState.IsConnected)
                {
                    //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    //playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Detect if left button was pressed this frame
        if (prevState.DPad.Left == ButtonState.Released && prevState.DPad.Left == ButtonState.Pressed)
        {
            leftPressed = true;
            //GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if left button was released this frame
        if (prevState.DPad.Left == ButtonState.Pressed && prevState.DPad.Left == ButtonState.Released)
        {
            leftPressed = false;
            //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        // Detect if left button was pressed this frame
        if (prevState.DPad.Right == ButtonState.Released && prevState.DPad.Right == ButtonState.Pressed)
        {
            rightPressed = true;
            //GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if left button was released this frame
        if (prevState.DPad.Right == ButtonState.Pressed && prevState.DPad.Right == ButtonState.Released)
        {
            rightPressed = false;
            //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }


        //////////////////////////Left and right movement
        //left:
        if (leftPressed)
        {
            rb.AddForce(Vector2.left * runSpeed, ForceMode2D.Impulse);
        }
        if (rightPressed)
        {
            rb.AddForce(Vector2.right * runSpeed, ForceMode2D.Impulse);
        }
    }
}
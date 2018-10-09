using System.Collections;
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
        rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        rb.freezeRotation = true;
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)PlayerID;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);


        // Detect if left button was pressed this frame
        if (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed)
        {
            Debug.Log("Left pressed");
            leftPressed = true;
            //GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if left button was released this frame
        if (prevState.DPad.Left == ButtonState.Pressed && state.DPad.Left == ButtonState.Released)
        {
            leftPressed = false;
            Debug.Log("Left released");
            //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        // Detect if left button was pressed this frame
        if (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed)
        {
            Debug.Log("Right pressed");
            rightPressed = true;
            //GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if left button was released this frame
        if (prevState.DPad.Right == ButtonState.Pressed && state.DPad.Right == ButtonState.Released)
        {
            Debug.Log("Right released");
            rightPressed = false;
            //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }


        //////////////////////////Left and right movement
        //left:
        if (!isDead)
        {
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
}

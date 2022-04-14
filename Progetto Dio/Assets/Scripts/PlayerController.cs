using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private int playerID = 0;
    public Vector3 moveInput;
    [HideInInspector] public CharacterController cc;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        player= ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
    }

    void GatherInput()
    {
        moveInput = GetMoveInput();
        Move();
    }
    Vector3 GetMoveInput()
    {
        return new Vector3(player.GetAxis("XAxis"),0,player.GetAxis("YAxis"));
    }
    void Move()
    {
        cc.Move(moveInput * speed * Time.deltaTime);
    }
}

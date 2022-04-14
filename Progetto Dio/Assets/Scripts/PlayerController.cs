using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    private int playerID = 0;
    private Player player;

    [HideInInspector] public CharacterController cc;
    
    [Header("Movement")]
    public Vector3 moveInput;
    public float speed;

    [Header("Gravity")]
    public float gravity = -9.81f;
    public LayerMask floorMask;
    public Vector3 groundSpherePosition;
    public float groundSphereRadius;
    public bool isGrounded;
    private Vector3 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        player= ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded=CheckGravity(); //Calls checksphere each frame, do it better
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
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        cc.Move(moveInput * speed * Time.deltaTime);
        cc.Move(velocity*Time.deltaTime);
        if (isGrounded) velocity.y = -2;
    }
    bool CheckGravity()
    {
        if (Physics.CheckSphere(transform.position + groundSpherePosition, groundSphereRadius, floorMask)) return true;
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position+groundSpherePosition, groundSphereRadius);
    }
}

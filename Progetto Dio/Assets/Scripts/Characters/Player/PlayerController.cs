using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    private int playerID = 0;
    private Player player;

    [HideInInspector] public CharacterController cc;

    [Header("Camera")]
    public GameObject cameraHolder;
    public float cameraSpeed;

    [Header("Movement")]
    [HideInInspector] public Vector3 moveInput;
    public float speed;

    [Header("Gravity")]
    public float gravity = -9.81f;
    public LayerMask floorMask;
    public float groundSphereY;
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
        GatherInput();
    }

    void GatherInput()
    {
        moveInput = GetMoveInput();
        cameraHolder.transform.Rotate(new Vector3(0,CameraInput(),0)*Time.deltaTime*cameraSpeed);
        transform.LookAt(cameraHolder.transform);
        Move();
    }
    Vector3 GetMoveInput()
    {
        float horizontalAxis = player.GetAxis("XAxis");
        float verticalAxis = player.GetAxis("YAxis");
        Vector3 forward = cameraHolder.transform.forward;
        Vector3 right = cameraHolder.transform.right;
        forward.Normalize();
        right.Normalize();
        return forward * verticalAxis + right * horizontalAxis;
        
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

    public float CameraInput()
    {
        Debug.Log(player.GetAxis("MoveCameraX"));
        return player.GetAxis("MoveCameraX");
    }


    private void OnDrawGizmos()
    {
    }
}

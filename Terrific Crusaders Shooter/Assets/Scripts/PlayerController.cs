using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] CharacterController controller;

    [SerializeField] float playerSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;
    [SerializeField] int jumpMax;

    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] GameObject cube;

    private Vector3 playerVelocity;
    private int timesJumped;
    bool isShooting;

    private void Start()
    {
        
    }

    void Update()
    {
        movement();
        StartCoroutine(shoot());

    }

    void movement()
    {
        //is on the ground, reset Y velocity, reset jump counter
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }

        //first-person get inputs for ground movement
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Jumping
        if (Input.GetButtonDown("Jump") && timesJumped < jumpMax)
        {
            timesJumped++;
            playerVelocity.y = jumpHeight;
        }

        //Gravity
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    //
    IEnumerator shoot()
    {
        if(Input.GetButton("Shoot") && !isShooting)
        {
            isShooting = true;
            RaycastHit hit;


            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)),out hit, shootDist))
            {
                //Creat Cube
                //
                //Instantiate(cube, hit.point, cube.transform.rotation);
                //
                Instantiate(cube, hit.point, Camera.main.transform.rotation);
            }

            yield return new WaitForSeconds(shootRate);
            isShooting = false;
        }
        

    }

}
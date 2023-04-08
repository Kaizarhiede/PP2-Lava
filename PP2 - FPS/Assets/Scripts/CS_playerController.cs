using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_playerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;
    [SerializeField] int jumpsMax;

    [SerializeField] float shootRate;
    [SerializeField] int shootDistance;
    [SerializeField] GameObject cube;

    int jumpedTimes;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    bool isShooting;
    Vector3 move;

    private void Start()
    {

    }

    void Update()
    {
        movement();
        if(!isShooting && Input.GetButton("Shoot"))
            StartCoroutine(shoot());
    }

    void movement()
    {

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpedTimes = 0;
        }

        //move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && jumpedTimes < jumpsMax)
        {
            jumpedTimes++;
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    IEnumerator shoot()
    {
        isShooting = true;

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDistance))
        {

            Instantiate(cube, hit.point, transform.rotation);

        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;

    }

}
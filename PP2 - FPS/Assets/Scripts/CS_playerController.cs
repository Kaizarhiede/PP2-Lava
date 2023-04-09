using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_playerController : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Player Stats -----")]
    [Range(3, 8)] [SerializeField] float playerSpeed;
    [Range(5, 10)] [SerializeField] float jumpHeight;
    [Range(5, 20)] [SerializeField] float gravityValue;
    [Range(0, 2)] [SerializeField] int jumpsMax;

    [Header("----- Gun Stats -----")]
    [Range(3, 8)] [SerializeField] int shootDamage;
    [Range(0.1f, 3)] [SerializeField] float shootRate;
    [Range(5, 20)] [SerializeField] int shootDistance;
    //[SerializeField] GameObject cube;

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
        if (!CS_gameManager.instance.isPaused)
        {

            movement();

            if (!isShooting && Input.GetButton("Shoot"))
                StartCoroutine(shoot());
        }
        
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
            //if(hit.transform.CompareTag("Damageable"))
            //{
            //do something
            //}
            //Instantiate(cube, hit.point, transform.rotation);

            CS_IDamage damageable = hit.collider.GetComponent<CS_IDamage>();

            if(damageable != null)
            {
                damageable.takeDamage(shootDamage);
            }

        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;

    }

}
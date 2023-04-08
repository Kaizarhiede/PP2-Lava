using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_cameraControls : MonoBehaviour
{

    [SerializeField] int sensitivityHorizontal;
    [SerializeField] int sensitivityVertical;

    [SerializeField] int lockVerticalMinimum;
    [SerializeField] int lockVerticalMaximum;

    [SerializeField] bool invertY;

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityVertical;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityHorizontal;

        //Converting input to rotation float and option for inverting Y
        if (invertY)
            xRotation += mouseY;
        else
            xRotation -= mouseY;

        // clamp on camera rotation
        xRotation = Mathf.Clamp(xRotation, lockVerticalMinimum, lockVerticalMaximum);

        //rotate the camera on the X axis
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //rotate the player on the Y axis
        transform.parent.Rotate(Vector3.up * mouseX);

    }
}
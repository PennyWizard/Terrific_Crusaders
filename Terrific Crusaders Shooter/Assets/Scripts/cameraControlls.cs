using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControlls : MonoBehaviour
{
    [SerializeField] int sensHort;
    [SerializeField] int sensVert;

    [SerializeField] int lockVertMin;
    [SerializeField] int lockVertMax;

    public GameManager gameManager;

    public bool invert;

    float xRotation;
    float yRotation;
   

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
   
    // Update is called once per frame
    void LateUpdate()
    {

        input(gameManager);

        //clamp camera rotation
        xRotation = Mathf.Clamp(xRotation, lockVertMin, lockVertMax);

        // rotate the camera on the x-axis
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // rotate player
        GameManager.instance.player.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        
    }

    private void input(GameManager gameManager)
    {
        if (!gameManager.isPaused)
        {
            //get input
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensHort;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensVert;

            if (invert)
            {
                xRotation += mouseY;
                yRotation -= mouseX;
            }
            else
            {
                xRotation -= mouseY;
                yRotation += mouseX;
            }
        }
    }
   
}

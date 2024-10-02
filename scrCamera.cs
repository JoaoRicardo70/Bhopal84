using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCamera : MonoBehaviour
{
    //regular fov
    
    [SerializeField] Transform player;


    //modo 1
    float xRotation = 0f;

    //modo 2
    [SerializeField][Range (0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float sensitivity=100f;
    public Vector2 currentMouseDelta;
    Vector2 veloCurrentMouseDelta;
    float cameraCap;
   
  

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        // float mouseX= Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        // float mouseY= Input.GetAxis("Mouse Y")* sensitivity * Time.deltaTime;

        // xRotation-=mouseY;
        // xRotation= Mathf.Clamp(xRotation,-90f,90f);
        // transform.localRotation=Quaternion.Euler(xRotation,0f,0f);
        // player.Rotate(Vector3.up * mouseX);
        //TODO: travar quando estiver conversando.
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        currentMouseDelta= Vector2.SmoothDamp(currentMouseDelta, mouseDelta, ref veloCurrentMouseDelta, mouseSmoothTime);
        cameraCap-=currentMouseDelta.y * sensitivity;
        cameraCap= Mathf.Clamp(cameraCap,-90.0f,90.0f);
        transform.localEulerAngles = Vector3.right * cameraCap;
        player.Rotate(Vector3.up * currentMouseDelta.x * sensitivity);
    }
}

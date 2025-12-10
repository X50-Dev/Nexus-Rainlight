using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject CameraPoint;
    [SerializeField]private GameObject Player;
    private Rigidbody2D rb;
    [SerializeField]private Vector3 Velocity = Vector3.zero;
    [SerializeField] private Vector3 Target;
    [SerializeField]private float Speed;

    [SerializeField] private float CamSize;

    [SerializeField]float VelocityY ;

    void Start()
    {
        Camera = new GameObject("CameraPlayer").AddComponent<Camera>();
        Camera.orthographic = true;
        Camera.clearFlags = CameraClearFlags.Skybox;
        Camera.depth = -1;
        Camera.allowMSAA = true;
        Camera.useOcclusionCulling = false;


        //Player = gameObject;
        rb = Player.GetComponent<Rigidbody2D>();
        VelocityY = rb.velocity.y;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.orthographicSize = CamSize;
        VelocityY = Mathf.Clamp(rb.velocity.y * 0.5f, -1, 1);
        //CameraPoint.transform.localPosition = new Vector3(1, 2 * VelocityY, 0); 

            Target = CameraPoint.transform.position + new Vector3(0, 0, -10);
        Camera.transform.position = Vector3.SmoothDamp(Camera.transform.position, Target, ref Velocity, Speed);
    }
}

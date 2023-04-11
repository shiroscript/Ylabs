using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction { x, y, z }
public class CameraRotater : MonoBehaviour
{
    public new Camera camera;
    public Transform rotator;
    public Transform lookatTarget;
    public float rotateSpeed = 0.5f;
    [SerializeField] Direction direction = Direction.y;
    [SerializeField] bool _lockPos;

    // Start is called before the first frame update
    void Start()
    {
        //camera.transform.SetParent(rotator);
        camera.transform.LookAt(lookatTarget);
    }

    // Update is called once per frame
    void Update()
    {
        if (_lockPos)
            return;

        RotateCam();
    }

    void RotateCam()
    {
        var localRotationOri = rotator.localRotation.eulerAngles;

        switch (direction)
        {
            case Direction.x:
                localRotationOri += new Vector3(rotateSpeed, 0, 0);
                break;
            case Direction.z:
                localRotationOri += new Vector3(0, 0, rotateSpeed);
                break;
            case Direction.y:
            default:
                localRotationOri += new Vector3(0, rotateSpeed, 0);
                break;
        }
        rotator.localRotation = Quaternion.Euler(localRotationOri);
    }
}

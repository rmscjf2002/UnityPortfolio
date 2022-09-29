using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어를 따라다니는 Camera
public class FollowCamera : MonoBehaviour
{

    public Transform target;
    public Vector3 offSet;

    void Update()
    {
        transform.position = target.position + offSet;
    }
}

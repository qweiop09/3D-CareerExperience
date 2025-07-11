using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _targetPlayer;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPlayer.position, 0.2f)
            - Vector3.forward + Vector3.up * 0.25f;
    }
}

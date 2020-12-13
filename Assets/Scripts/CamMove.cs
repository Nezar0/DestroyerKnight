using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CamMove : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;

    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -164f, 164f),
            Mathf.Clamp(targetToFollow.position.y, -7.6f, 7.6f),
            transform.position.z
            );

    }
}

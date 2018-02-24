using UnityEngine;


public class SmoothFollow : MonoBehaviour
{
    public Transform TargetPlayer;
    public Vector3 Offset;

    void Update()
    {
        transform.position = new Vector3(TargetPlayer.position.x + Offset.x, TargetPlayer.position.y + Offset.y, Offset.z); // Camera follows the player with specified offset position
    }
}


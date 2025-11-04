using UnityEngine;

public class SimpleCameraFollow2D : MonoBehaviour
{
    [Header("÷ель дл€ камеры")]
    public Transform target;  // игрок

    [Header("—м€гчение движени€")]
    public float smoothSpeed = 0.125f; // чем меньше Ч тем плавнее
    public Vector3 offset; // смещение камеры относительно игрока

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // ѕлавное движение камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //  амера не двигаетс€ по Z
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}

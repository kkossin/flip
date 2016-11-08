using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private float orthographicSize = 5f;
    private float aspect = 1.77777f;
    void Start()
    {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(
                -orthographicSize * aspect, orthographicSize * aspect,
                -orthographicSize, orthographicSize,
                GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);
    }
}

using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public GameObject plane;
    public GameObject sphere;
    public GameObject cube;

    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;
    public float scaleSpeed = 1f;

    void Update()
    {
        // Cube Movement (Left, Right, Up, Down)
        if (cube != null)
        {
            float moveX = Input.GetAxis("Horizontal"); // A, D or Left Arrow, Right Arrow
            float moveY = Input.GetAxis("Vertical"); // W, S or Up Arrow, Down Arrow
            cube.transform.Translate(new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime);
        }

        // Sphere Scaling (Increase/Decrease Size)
        if (sphere != null)
        {
            if (Input.GetKey(KeyCode.W)) 
                sphere.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.S)) 
                sphere.transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
        }

        // Plane Rotation
        if (plane != null)
        {
            float rotate = 0f;
            if (Input.GetKey(KeyCode.UpArrow)) rotate = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) rotate = -1f;

            plane.transform.Rotate(Vector3.forward * rotate * rotateSpeed * Time.deltaTime);
        }
    }
}

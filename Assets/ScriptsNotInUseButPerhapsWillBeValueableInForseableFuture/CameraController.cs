using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panspeed = 12;
    public float screenBoarderThickness = 10;
    public Vector2 mapLimit;
    //float speed = 5;
    //float cameraRotateSpeed = 1;
    public float scrollspeed = 20000;
    float minimumHeight = 8, maximumHeight = 25;

    //float yaw = 0.0f, pitch = 0.0f;

    float test = 1;
    // Update is called once per frame
    void Update() {

        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");

        Vector3 position = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - screenBoarderThickness) {
            position.z += panspeed * Time.deltaTime;

            //position += Vector3.forward * speed * Time.deltaTime;
            //camera.transform.Translate(Vector3.forward * Time.deltaTime);
            //transform.position += this.transform.forward * speed * Time.deltaTime;s
            //transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
            //transform.position += transform.forward * Time.deltaTime * 50000000;


        }
        if (Input.GetKey("s") || Input.mousePosition.y <= screenBoarderThickness) {
            position.z -= panspeed * Time.deltaTime;

        }
        if (Input.GetKey("d")) {
            position.x += panspeed * Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            position.x -= panspeed * Time.deltaTime;
        }

        position.x = Mathf.Clamp(position.x, -mapLimit.x, mapLimit.x);
        position.z = Mathf.Clamp(position.z, -mapLimit.y, mapLimit.y);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        position.y -= scroll * scrollspeed * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, minimumHeight, maximumHeight);
        transform.position = position;



        if (Input.mousePosition.x >= Screen.width - screenBoarderThickness) {
            test++;
            transform.rotation = Quaternion.Euler(50, test, 0);
        }

        if (Input.mousePosition.x <= screenBoarderThickness) {
            test--;
            transform.rotation = Quaternion.Euler(50, test, 0);
        }
    }
}
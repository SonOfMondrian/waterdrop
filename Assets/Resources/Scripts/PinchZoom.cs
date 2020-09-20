using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour
{
    public float minZoom = 1f;
    public float maxZoom = 10f;
    public float sensitivity = 2f;
    public float cammovesensitivity;
    Vector3 cameraPosition;
    Vector3 mousePositionOnScreen;
    Vector3 mousePositionOnScreen1;
    Vector3 camPos1;
    Vector3 mouseOnWorld;
    Vector3 camDragBegin;
    Vector3 camDragNext;
    public Vector3 posDiff;


    float lastdis, newdis;

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = Camera.main.transform.position;
        mousePositionOnScreen = new Vector3();
        mousePositionOnScreen1 = new Vector3();
        camPos1 = new Vector3();
        mouseOnWorld = new Vector3();
    }

    void FixedUpdate()
    {
        mouseOnWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            mousePositionOnScreen = mousePositionOnScreen1;
            mousePositionOnScreen1 = Input.mousePosition;

            Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray1.origin, ray1.direction * 10000f, Color.yellow, 1f);

            float fov = Camera.main.orthographicSize;
            fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minZoom, maxZoom);
            Camera.main.orthographicSize = fov;
            Vector3 mouseOnWorld1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 posDiff = mouseOnWorld - mouseOnWorld1;
            Vector3 camPos = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(camPos.x + posDiff.x, camPos.y + posDiff.y, camPos.z);

        }

        if (Input.touchCount == 2)
        {
            Ray ray1 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);
            ray1.direction.Normalize();
            ray2.direction.Normalize();
            //print("ray1: "+ray1.ToString("f4"));

            Debug.DrawRay(ray1.origin, ray1.direction * 100f, Color.yellow, 0.01f);
            Debug.DrawRay(ray2.origin, ray2.direction * 100f, Color.yellow, 0.01f);
            Ray ray3 = new Ray(ray1.origin, ray1.direction + ray2.direction);
            ray3.direction.Normalize();
            Debug.DrawRay(ray3.origin, ray3.direction * 100f, Color.yellow, 0.01f);


            float distance = Vector3.Distance(ray1.direction, ray2.direction);
            //print(distance);

            if (lastdis == 0)
            {
                lastdis = distance;
                return;
            }
            newdis = distance;
            float delta = lastdis - newdis;
            print(delta);
            //FOV 연산
            float fov = Camera.main.fieldOfView;
            fov += delta * sensitivity;
            fov = Mathf.Clamp(fov, minZoom, maxZoom);
            Camera.main.fieldOfView = fov;
            lastdis = newdis;




            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                lastdis = 0;
            }

        }


        //start Pan or move objects at mouse position

        if (Input.GetMouseButtonDown(2))
        {
            camDragBegin = Input.mousePosition;
            camPos1 = Camera.main.transform.position;
        }
        // Pan or move objects at mouse position

        if (Input.GetMouseButton(2))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {

                camDragNext = Input.mousePosition;
                Vector3 screenDelta = camDragBegin - camDragNext;
                Vector2 screenSize = ScaleScreenToWorldSize(Camera.main.aspect, Camera.main.orthographicSize, Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight, screenDelta.x, screenDelta.y);

                Vector3 camPosMove = new Vector3(camPos1.x + screenSize.x, camPos1.y + screenSize.y, camPos1.z);
                Camera.main.transform.position = camPosMove;
            }
        }
    }

    // convert screen coordinate to world coordinate
    Vector2 ScaleScreenToWorldSize(float camAspect, float camSize, float camScreenPixelWidth, float camScreenPixelHeight, float screenW, float screenH)
    {
        float cameraWidth = camAspect * camSize * 2f;
        float cameraHeght = camSize * 2f;
        float screenWorldW = screenW * (cameraWidth / camScreenPixelWidth);
        float screenWorldH = screenH * (cameraHeght / camScreenPixelHeight);

        return new Vector2(screenWorldW, screenWorldH);

    }
}
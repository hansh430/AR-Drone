using UnityEngine;

public class BasketballThrow : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float dragLimit =3f;
    [SerializeField] private float forceToAdd = 10f;
    private Camera cam;
    private bool isDragging;
    private Vector3 MousePosition
    {
        get
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }

    void Start()
    {
        cam = Camera.main;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, Vector2.zero);
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isDragging)
        {
            DragStart();
        }
        if(isDragging)
        {
            Drag();
        }
        if(Input.GetMouseButtonUp(0) && isDragging)
        {
            DragEnd();
        }
    }

    void DragStart()
    {
        lineRenderer.enabled = true;    
        isDragging = true;
        lineRenderer.SetPosition(0, MousePosition);
    }
    void Drag()
    {
        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 currentPos = MousePosition;
        Vector3 distance =currentPos- startPos;
        if(distance.magnitude<= dragLimit)
        {
            lineRenderer.SetPosition(1, currentPos);
        }
        else
        {
            Vector3 limitVector = startPos +(distance.normalized*dragLimit);
            lineRenderer.SetPosition(0, limitVector);
        }
    }
    void DragEnd()
    {
        isDragging = false;
        lineRenderer.enabled = false;
    }
   
}

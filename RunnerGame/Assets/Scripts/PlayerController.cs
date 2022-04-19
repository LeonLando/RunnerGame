using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    [SerializeField] private RoadGenerator _roadGenerator;
    Vector3 StartGamePosition;
    Quaternion StartGameRotation;
    float laneOffset = 3f;
    float laneChangeSpeed = 5;
    Rigidbody rb;
    Vector3 targetVelocity;
    float PointStart;
    float PointFinish;
    bool isMoving = false;
    Coroutine MovingCoroutine;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        StartGamePosition = transform.position;
        StartGameRotation = transform.rotation;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && PointFinish > -laneOffset)
        {
            MoveHorizontal(-laneChangeSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D) && PointFinish < laneOffset)
        {
            MoveHorizontal(laneChangeSpeed);
            
        }
    }

    void MoveHorizontal(float speed)
    {
        anim.applyRootMotion = false;
        PointStart = PointFinish;
        PointFinish += Mathf.Sign(speed) * laneOffset;

        if (isMoving)
        {
            StopCoroutine(MovingCoroutine);
            isMoving = false;   
        }
       MovingCoroutine = StartCoroutine(MoveCoroutine(speed));

    }

    IEnumerator MoveCoroutine(float Vectorx)
    {
       isMoving = true;
        while (Mathf.Abs(PointStart - transform.position.x) < laneOffset)
        {
            yield return new WaitForFixedUpdate();

            rb.velocity = new Vector3(Vectorx, rb.velocity.y, 0);
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(PointStart, PointFinish), Mathf.Max(PointStart, PointFinish));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        rb.velocity = Vector3.zero; 
        transform.position = new Vector3(PointFinish, transform.position.y, transform.position.z);
        isMoving = false;
       

    }
    public void StartGame()
    {
        anim.SetTrigger("Run");
        _roadGenerator.StartLevel();
    }
    public void ResetGame()
    {
        anim.SetTrigger("Idle");
        transform.position = StartGamePosition;
        transform.rotation = StartGameRotation; 
        _roadGenerator.ResetLevel();    
    }
}

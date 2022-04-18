using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    public enum Direction {Left, Right, Up, Down };
    bool[] Swipe = new bool[4];
    
    Vector2 StartTouch;
    bool touchMoved;
    Vector2 SwipeDelta;
    const float SwipeTreshold = 50;

    public delegate void MoveDelegate(bool[] Swipes);
    public MoveDelegate MoveEvent;
    public delegate void ClickDelegate(Vector2 pos);
    public ClickDelegate ClickEvent;

    private void Awake()
    {
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (TouchBegan())
        {
            StartTouch = TouchPosition();
            touchMoved = true;  
        }
        else if (TouchEnded() && touchMoved == true)
        {
            SendSwipe();
            touchMoved=false;   
        }

        SwipeDelta = Vector2.zero;
        if (touchMoved && GetTouch())
        {
            SwipeDelta = TouchPosition() - StartTouch;  
        }

        if (SwipeDelta.magnitude > SwipeTreshold)
        {
            if (Mathf.Abs(SwipeDelta.x) > Mathf.Abs(SwipeDelta.y))
            {
                Swipe[(int)Direction.Left] = SwipeDelta.x < 0;
                Swipe[(int)Direction.Right] = SwipeDelta.x > 0;
            }
            else
            {
                Swipe[(int)Direction.Down] = SwipeDelta.y < 0;
                Swipe[(int)Direction.Up] = SwipeDelta.y > 0;
            }
            SendSwipe();
        }
    }

    Vector2 TouchPosition()
    {
        return (Vector2)Input.mousePosition;
    }

    bool TouchBegan()
    { 
     return Input.GetMouseButtonDown(0);  
    }
    bool TouchEnded()
    {
        return Input.GetMouseButtonUp(0);
    }
    bool GetTouch()
    {
        return Input.GetMouseButton(0);
    }

    void SendSwipe()
    {
        if (Swipe[0] || Swipe[1] || Swipe[2] || Swipe[3])
        {
            Debug.Log(Swipe[0] + "|" + Swipe[1] + "|" + Swipe[2] + "|" + Swipe[3]);
                MoveEvent?.Invoke(Swipe);
        }
        else
        {
            Debug.Log("Click");
            ClickEvent?.Invoke(TouchPosition());
        }
        Reset();
    }

    private void Reset()
    {
        StartTouch = SwipeDelta = Vector2.zero;
        touchMoved = false;
        for (int i = 0; i < 4; i++)
        {
            Swipe[i] = false;
        }
    }
}

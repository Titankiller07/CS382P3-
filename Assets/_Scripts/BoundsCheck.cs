using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [System.Flags]
    public enum eScreenLocs { 
        onScreen = 0,
        offLeft = 1,
        offRight = 2,
        offUp = 4,
        offDown = 8
     };
    public enum eType { center, inset, outset};

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;
    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    public float camWidth;
    public float camHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

   void LateUpdate()
{
    float checkRadius = 0;
    if(boundsType == eType.center)
    {
        checkRadius = -radius;
    }
    if(boundsType == eType.outset)
    {
        checkRadius = radius;
    }

    Vector3 pos = transform.position;
    screenLocs = eScreenLocs.onScreen;

    if (pos.x > camWidth - checkRadius)
    {
        pos.x = camWidth - checkRadius;
        screenLocs |= eScreenLocs.offRight;
    }
    if (pos.x < -camWidth + checkRadius)
    {
        pos.x = -camWidth + checkRadius;
        screenLocs |= eScreenLocs.offLeft;
    }
    if (pos.y > camHeight - checkRadius)
    {
        pos.y = camHeight - checkRadius;
        screenLocs |= eScreenLocs.offUp;
    }
    if (pos.y < -camHeight + checkRadius)
    {
        pos.y = -camHeight + checkRadius;
        screenLocs |= eScreenLocs.offDown;
    }

    if(keepOnScreen && !isOnScreen)
    {
        transform.position = pos;
        screenLocs = eScreenLocs.onScreen;
    }
}

    public bool isOnScreen
    {
        get
        {
            return (screenLocs == eScreenLocs.onScreen);
        }
    }
    public bool LocIs(eScreenLocs checkLoc)
    {
        if(checkLoc == eScreenLocs.onScreen) return isOnScreen;
        return ((screenLocs & checkLoc) == checkLoc);
    }
}
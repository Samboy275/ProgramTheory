using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    // singlton
    public static CursorManager instance { get; private set; }
    [SerializeField]private  Texture2D lockOnCursor;
    
    private Vector2 cursorSpot;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    public void SetCursor()
    {
        cursorSpot = new Vector2(lockOnCursor.width /2 , lockOnCursor.height / 2);
        Cursor.SetCursor(lockOnCursor, cursorSpot, CursorMode.Auto);
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

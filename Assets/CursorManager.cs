using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CursorImage
{
    public string name;
    public Texture2D texture;
}
public class CursorManager : MonoBehaviour
{
    [SerializeField] List<CursorImage> cursorImages;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(FindImage("MoveArrow"), Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D FindImage(string name)
    {
        return cursorImages.Find(x => x.name == name).texture;
    }
}

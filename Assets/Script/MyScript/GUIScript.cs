using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScript : MonoBehaviour {
    static Texture2D _whiteTexture;

    public static Texture2D WhiteTexture {
        get {
            if (_whiteTexture == null) {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }
            return _whiteTexture;
        }
    }
    public static void DrawScreenRect(Rect rect, Color color) {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }
    public static void DrawScreenRectBorder(Rect rectangle, float borderThickness, Color color) {
        // Top
        DrawScreenRect(new Rect(rectangle.xMin, rectangle.yMin, rectangle.width, borderThickness), color);
        //Left
        DrawScreenRect(new Rect(rectangle.xMin, rectangle.yMin, borderThickness, rectangle.height), color);
        // Right
        DrawScreenRect(new Rect(rectangle.xMax - borderThickness, rectangle.yMin, borderThickness, rectangle.height), color);
        // Bottom
        DrawScreenRect(new Rect(rectangle.xMin, rectangle.yMax - borderThickness, rectangle.width, borderThickness), color);
    }
    void OnGUI() {
        // Left example
        DrawScreenRectBorder(new Rect(32, 32, 256, 128), 2, Color.green);
        // Right example
        DrawScreenRect(new Rect(320, 32, 256, 128), new Color(0.8f, 0.8f, 0.95f, 0.25f));
        DrawScreenRectBorder(new Rect(320, 32, 256, 128), 2, new Color(0.8f, 0.8f, 0.95f));
    }

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2) {
        //Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        //Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        //Create rectangle
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2) {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);

        return bounds;
    }
    void Update() {

    }
}

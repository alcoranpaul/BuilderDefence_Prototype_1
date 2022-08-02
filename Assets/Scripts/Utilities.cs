using UnityEngine;

public static class Utilities {
    private static Camera m_Camera;
    public static Vector3 GetMousePosition() {
        if (m_Camera == null) m_Camera = Camera.main;

        Vector3 mousePosition_Pixel = Input.mousePosition;
        Vector3 mousePosition_World = m_Camera.ScreenToWorldPoint(mousePosition_Pixel);
        mousePosition_World.z = 0f; //No need for z-axis
        return mousePosition_World;
    }
}

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

    public static Vector3 GetRandomDirection() {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    public static float GetAngleFromVector(Vector3 vector) {
        float rotation = Mathf.Atan2(vector.y, vector.x);
        float degrees = rotation * Mathf.Rad2Deg;
        return degrees;
    }
}

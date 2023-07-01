using System.Collections;
using UnityEngine;

public static class DebugUtils
{
    public static IEnumerator DrawDebugCircle(Vector2 center, float radius, Color color, float duration)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            yield return null;
            timePassed += Time.deltaTime;
            const float fullCircle = 2f * Mathf.PI;
            const int segments = 36; // Number of line segments to approximate the circle

            float angleIncrement = fullCircle / segments;

            Vector2 previousPoint = center + new Vector2(radius, 0f);
            for (int i = 1; i <= segments; i++)
            {
                float angle = i * angleIncrement;
                float x = center.x + Mathf.Cos(angle) * radius;
                float y = center.y + Mathf.Sin(angle) * radius;
                Vector2 currentPoint = new Vector2(x, y);
                Debug.DrawLine(previousPoint, currentPoint, color);
                previousPoint = currentPoint;
            }
        }
    }
}

using System.Collections;
using UnityEngine;


public static class PointGenerator
{
    public static Vector2 GetRandomPointInRadius(Vector2 center, float radius)
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float randomRadius = radius * Random.Range(0.1f, 1f);

        float x = center.x + randomRadius * Mathf.Cos(randomAngle);
        float y = center.y + randomRadius * Mathf.Sin(randomAngle);

        return new Vector2(x, y);
    }

    public static Vector2 GetRandomPointInArea(Vector2 center, float radius, Collider2D spawnArea)
    {
        while (true)
        {
            Vector2 point = GetRandomPointInRadius(center, radius);
            if (spawnArea.OverlapPoint(point))
            {
                return point;
            }
        }
    }

}

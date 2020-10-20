using UnityEngine;
using UnityEditor;

public class ColliderEditor
{
    [MenuItem("Tools/LineRenderer points -> PolygonCollider2D", false)]
    private static void CopyLineToCollider()
    {
        var line = Selection.activeGameObject.GetComponent<LineRenderer>();
        var collider = Selection.activeGameObject.GetComponent<PolygonCollider2D>();

        var positions = new Vector2[line.positionCount];
        for (int i = 0; i < line.positionCount; i++)
        {
            positions[i] = (Vector2)line.GetPosition(i);
        }

        collider.points = positions;
    }

    [MenuItem("Tools/LineRenderer points -> PolygonCollider2D", true)]
    private static bool CopyLineToColliderValidate()
    {
        return Selection.activeGameObject && Selection.activeGameObject.GetComponent<LineRenderer>() && Selection.activeGameObject.GetComponent<PolygonCollider2D>();
    }

    [MenuItem("Tools/PolygonCollider2D points -> LineRenderer", false)]
    private static void CopyColliderToLine()
    {
        var line = Selection.activeGameObject.GetComponent<LineRenderer>();
        var collider = Selection.activeGameObject.GetComponent<PolygonCollider2D>();

        var positions = collider.points;
        line.positionCount = positions.Length;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, positions[i]);
        }
    }

    [MenuItem("Tools/PolygonCollider2D points -> LineRenderer", true)]
    private static bool CopyColliderToLineValidate()
    {
        return Selection.activeGameObject && Selection.activeGameObject.GetComponent<LineRenderer>() && Selection.activeGameObject.GetComponent<PolygonCollider2D>();
    }
}


using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;


public static class ShadowCasterFromTilemap
{
    //changes shape of shadow caster by replacing pat
    //does not update mesh which actually casts the shadow
    public static void SetPath(this ShadowCaster2D shadowCaster, Vector3[] path)
    {
        FieldInfo shapeField = typeof(ShadowCaster2D).GetField("m_ShapePath",
            BindingFlags.NonPublic |
            BindingFlags.Instance);
        shapeField.SetValue(shadowCaster, path);
    }
    
    //triggers shadow caster to rebuild internal data like the mesh b/c hash was invalidated
    public static void SetPathHash(this ShadowCaster2D shadowCaster, int hash)
    {
        FieldInfo hashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash",
            BindingFlags.NonPublic |
            BindingFlags.Instance);
        hashField.SetValue(shadowCaster, hash);
    }
}



public class ShadowCaster2DGenerator
{

#if UNITY_EDITOR

    [UnityEditor.MenuItem("Generate Shadow Casters", menuItem = "Tools/Generate Shadow Casters")]
    public static void GenerateShadowCasters()
    {
        Tilemap[] tilemaps = GameObject.FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
        int len = tilemaps.Length;
        for(int i = 0; i < len; ++i)
        {
            GenerateTilemapShadowCastersInEditor(tilemaps[i], false);
        }
    }

    [UnityEditor.MenuItem("Generate Shadow Casters (Self Shadows)", menuItem = "Tools/Generate Shadow Casters (Self Shadows)")]
    public static void GenerateShadowCastersSelfShadows()
    {
        Tilemap[] tilemaps = GameObject.FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
        int len = tilemaps.Length;
        for(int i = 0; i < len; ++i)
        {
            GenerateTilemapShadowCastersInEditor(tilemaps[i], true);
        }
    }

    /// <summary>
    /// Given a Composite Collider 2D, it replaces existing Shadow Caster 2Ds (children) with new Shadow Caster 2D objects whose
    /// shapes coincide with the paths of the collider.
    /// </summary>
    /// <remarks>
    /// It is recommended that the object that contains the collider component has a Composite Shadow Caster 2D too.
    /// It is recommended to call this method in editor only.
    /// </remarks>
    /// <param name="collider">The collider which will be the parent of the new shadow casters.</param>
    /// <param name="selfShadows">Whether the shadow casters will have the Self Shadows option enabled..</param>
    public static void GenerateTilemapShadowCastersInEditor(Tilemap collider, bool selfShadows)
    {
        GenerateTilemapShadowCasters(collider, selfShadows);

        UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
    }

#endif

    /// <summary>
    /// Given a Composite Collider 2D, it replaces existing Shadow Caster 2Ds (children) with new Shadow Caster 2D objects whose
    /// shapes coincide with the tilemap
    /// </summary>
    /// <remarks>
    /// It is recommended that the object that contains the collider component has a Composite Shadow Caster 2D too.
    /// It is recommended to call this method in editor only.
    /// </remarks>
    /// <param name="tilemap">The collider which will be the parent of the new shadow casters.</param>
    /// <param name="selfShadows">Whether the shadow casters will have the Self Shadows option enabled..</param>
    public static void GenerateTilemapShadowCasters(Tilemap tilemap, bool selfShadows)
    {

        // GetTilemapPath(tilemap);
        // // First, it destroys the existing shadow casters
        // ShadowCaster2D[] existingShadowCasters = tilemap.GetComponentsInChildren<ShadowCaster2D>();
        //
        // for (int i = 0; i < existingShadowCasters.Length; ++i)
        // {
        //     if(existingShadowCasters[i].transform.parent != tilemap.transform)
        //     {
        //         continue;
        //     }
        //
        //     GameObject.DestroyImmediate(existingShadowCasters[i].gameObject);
        // }
        //
        // // Then it creates the new shadow casters, based on the paths of the composite collider
        // int pathCount = tilemap.pathCount;
        // List<Vector2> pointsInPath = new List<Vector2>();
        // List<Vector3> pointsInPath3D = new List<Vector3>();
        //
        // for (int i = 0; i < pathCount; ++i)
        // {
        //     tilemap.GetPath(i, pointsInPath);
        //
        //     GameObject newShadowCaster = new GameObject("ShadowCaster2D");
        //     newShadowCaster.isStatic = true;
        //     newShadowCaster.transform.SetParent(tilemap.transform, false);
        //
        //     for(int j = 0; j < pointsInPath.Count; ++j)
        //     {
        //         pointsInPath3D.Add(pointsInPath[j]);
        //     }
        //
        //     ShadowCaster2D component = newShadowCaster.AddComponent<ShadowCaster2D>();
        //     component.SetPath(pointsInPath3D.ToArray());
        //     component.SetPathHash(Random.Range(int.MinValue, int.MaxValue)); // The hashing function GetShapePathHash could be copied from the LightUtility class
        //     component.selfShadows = selfShadows;
        //     component.Update();
        //
        //     pointsInPath.Clear();
        //     pointsInPath3D.Clear();
        // }
    }



    public static void GetTilemapPath(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tiles = tilemap.GetTilesBlock(bounds);
        // Vector2[] positions = tiles[0].position;
    }
}

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ShaderOccurenceWindow : EditorWindow
{
    [MenuItem("Tools/Shader Occurence")]
    public static void Open()
    {
        GetWindow<ShaderOccurenceWindow>();
    }

    Shader shader;
    Shader shader2;
    List<string> materials = new List<string>();
    Vector2 scroll;

    void OnGUI()
    {
        Shader prev = shader;
        shader = EditorGUILayout.ObjectField(shader, typeof(Shader), false) as Shader;
        shader2 = EditorGUILayout.ObjectField(shader2, typeof(Shader), false) as Shader;
        string[] allMaterials = AssetDatabase.FindAssets("t:Material");
        var material = AssetDatabase.LoadAssetAtPath<Material>(allMaterials[i]);

        if (shader != prev)
        {
            string shaderPath = AssetDatabase.GetAssetPath(shader);
            materials.Clear();
            for (int i = 0; i < allMaterials.Length; i++)
            {
                allMaterials[i] = AssetDatabase.GUIDToAssetPath(allMaterials[i]);
                string[] dep = AssetDatabase.GetDependencies(allMaterials[i]);
                /*if (ArrayUtility.Contains(dep, shaderPath))
                    materials.Add(allMaterials[i]);*/
                GUILayout.BeginHorizontal();
                {
                    //GUILayout.Label(Path.GetFileNameWithoutExtension(materials[i]));
                    GUILayout.FlexibleSpace();
                    if (material.shader == shader)
                    {
                        materials.Add(allMaterials[i]);
                    }
                }
                GUILayout.EndHorizontal();


            }
        }


        scroll = GUILayout.BeginScrollView(scroll);
        {
            for (int i = 0; i < materials.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label(Path.GetFileNameWithoutExtension(materials[i]));
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Show"))
                        EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(materials[i], typeof(Material)));
                }
                GUILayout.EndHorizontal();
            }
        }

        //new stuff

        if (GUILayout.Button("Swap"))
        {
            for (int i = 0; i < allMaterials.Length; i++)
            {
                material.shader = shader2;
            }
        }

        GUILayout.EndScrollView();

        
    }
}
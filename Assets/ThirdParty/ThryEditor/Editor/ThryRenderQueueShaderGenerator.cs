﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThryRenderQueueShaderGenerator : EditorWindow
{
    // Add menu named "My Window" to the Window menu
    [MenuItem("Thry/Editor Tools/Render Queue Generator")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ThryRenderQueueShaderGenerator window = (ThryRenderQueueShaderGenerator)EditorWindow.GetWindow(typeof(ThryRenderQueueShaderGenerator));
        window.Show();
    }

    public static readonly int[] COMMON_QUEUES = new int[] { 0, 10, 20, 30, 100, 200, 300, 1000, 1990, 1995, 1996, 1997, 1998, 1999, 2000, 2001, 2002, 2003, 2004, 2005, 2010, 2440, 2445, 2446, 2447, 2448, 2449, 2450, 2451, 2452, 2453, 2454, 2455, 2460, 2990, 2995, 2996, 2997, 2998, 2999, 3000, 3001, 3002, 3004, 3005, 3010 };

    int createShadersFrom = 2000;
    int createShadersTo = 2010;

    int selectedShader = 0;
    string[] poiShaders;

    bool reload = true;

    private void OnGUI()
    {
        if (poiShaders == null || reload)
        {

            string[] shaderGuids = AssetDatabase.FindAssets("t:shader");
            List<string> poiShaders = new List<string>();
            foreach (string g in shaderGuids)
            {
                Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(AssetDatabase.GUIDToAssetPath(g));
                Material m = new Material(shader);
                if (m.HasProperty(Shader.PropertyToID("shader_is_using_thry_editor")))
                {
                    string defaultShaderName = ThryHelper.getDefaultShaderName(shader.name);
                    if (!poiShaders.Contains(defaultShaderName)) poiShaders.Add(defaultShaderName);
                }
            }
            this.poiShaders = new string[poiShaders.Count + 1];
            for (int i = 0; i < poiShaders.Count; i++) this.poiShaders[i + 1] = poiShaders[i];
        }

        GUILayout.Label("With thry editor the render queue selection in the material editor creates copies of the shader with different queues to make it work in vrchat. \n With this tool you can pre create those shader files so you don't have to wait.");

        Shader activeShader = ThrySettings.activeShader;

        if (activeShader != null) poiShaders[0] = ThryHelper.getDefaultShaderName(activeShader.name);
        else poiShaders[0] = "None";
        int newSelectShader = EditorGUILayout.Popup(0, poiShaders, GUILayout.MaxWidth(200));
        if (newSelectShader != selectedShader)
        {
            selectedShader = newSelectShader;
            activeShader = Shader.Find(poiShaders[newSelectShader]);
            ThrySettings.setActiveShader(activeShader);
        }

        if (activeShader != null)
        {
            string defaultShaderName = ThryHelper.getDefaultShaderName(activeShader.name); ;
            Shader defaultShader = Shader.Find(defaultShaderName);

            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.richText = true;
            EditorGUILayout.LabelField("<size=16>" + defaultShaderName + "</size>", style, GUILayout.MinHeight(18));

            GUILayout.Label("Generate Render Queue Shaders", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate All Queues", GUILayout.MaxWidth(200)))
            {
                for (int i = createShadersFrom; i <= createShadersTo; i++) { ThryHelper.createRenderQueueShaderIfNotExists(defaultShader, i, false); }
                AssetDatabase.Refresh();
            }
            GUILayout.Label("from", GUILayout.MaxWidth(30));
            createShadersFrom = EditorGUILayout.IntField(createShadersFrom, GUILayout.MaxWidth(50));
            GUILayout.Label("to", GUILayout.MaxWidth(15));
            createShadersTo = EditorGUILayout.IntField(createShadersTo, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Generate most common Queues", GUILayout.MaxWidth(200)))
            {
                foreach (int i in COMMON_QUEUES) { ThryHelper.createRenderQueueShaderIfNotExists(defaultShader, i, false); }
                AssetDatabase.Refresh();
            }
        }
    }
}

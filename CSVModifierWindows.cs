#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System.IO;

public class CSVModifierWindows : EditorWindow
{

    [MenuItem("CustomWindows/CSV Modifier")]
    public static void ShowWindows()
    {
        GetWindow<CSVModifierWindows>("CSV Modifier");
    }


    private enum WindowsState
    {
        Nothing,
        Editing,
    }
    WindowsState state = WindowsState.Nothing;

    //INTERNAL VARIABLE
    CSVFile currentEditingFile = null;
    TextAsset rawText = null;

    bool hasbeenLoad = false;


    //LINE PARAMETER
    int removingLineIndex = 0;
    int insertingIndex = -1;
    private void OnGUI()
    {
        TextAsset previousone = rawText;
        if(state == WindowsState.Nothing)
        {
            rawText = (TextAsset)EditorGUILayout.ObjectField("CSV file to edit", rawText, typeof(TextAsset),false);


        }
        else if(state == WindowsState.Editing)
        {
            rawText = (TextAsset)EditorGUILayout.ObjectField("CSV file to edit", rawText, typeof(TextAsset), false);
#region MODIFY BAR
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Line At (-1 to auto add at the end)"))
            {
                AddLine();
            }
            insertingIndex = EditorGUILayout.IntField(insertingIndex,GUILayout.MaxWidth(30));
            if(GUILayout.Button("Remove Line At"))
            {
                RemoveLine();
            }
            removingLineIndex = EditorGUILayout.IntField(removingLineIndex, GUILayout.MaxWidth(30));

            GUILayout.EndHorizontal();
#endregion
            GUILayout.Space(20);

            for (int i = 0; i < currentEditingFile.lines.Count; i++) {
#region ITEM
                GUILayout.BeginHorizontal();
                GUILayout.Label(i.ToString());
                for(int z = 0; z < currentEditingFile.lines[i].Length; z++)
                {
  
                    currentEditingFile.lines[i][z] = EditorGUILayout.TextField(currentEditingFile.lines[i][z]);
                }
                GUILayout.EndHorizontal();
#endregion

            }


            GUILayout.Space(30);
#region SAVING
            if(GUILayout.Button("Apply Changes"))
            {
                Apply();
            }
#endregion
        }

        if (rawText != previousone)
        {
   
            Init();
            state = WindowsState.Editing;
        }
        if(rawText == null)
        {
            state = WindowsState.Nothing;
        }
    }

    private void Init()
    {
        currentEditingFile = new CSVFile(rawText);

    }

    private void Apply()
    {
        currentEditingFile.Save(AssetDatabase.GetAssetPath(rawText));
       
    }

    private void AddLine()
    {
        if (insertingIndex == -1) currentEditingFile.AddLine();
        else currentEditingFile.AddLine(insertingIndex);

    }

    private void RemoveLine()
    {
        currentEditingFile.RemoveLine(removingLineIndex);
    }

}
#endif
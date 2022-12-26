using Game.Scripts.Data;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class CarSettingsEditorWindow : UnityEditor.EditorWindow
{
    private Editor _carSettingsEditor;
    private CarSettings _carSettings;

    [MenuItem("Settings/Car Settings")]
    private static void ShowWindow()
    {
        var window = GetWindow<CarSettingsEditorWindow>();
        window.titleContent = new GUIContent("Car Settings");
        window.Show();
    }

    private void OnEnable()
    {
        _carSettings =
            AssetDatabase.LoadAssetAtPath<ScriptableObject>(
                "Assets/Game/Data/CarSettingsData.asset") as CarSettings;

        Assert.IsNotNull(_carSettings);
        
        _carSettingsEditor = Editor.CreateEditor(_carSettings);
    }

    private void OnGUI()
    {
        _carSettingsEditor.OnInspectorGUI();
    }
}
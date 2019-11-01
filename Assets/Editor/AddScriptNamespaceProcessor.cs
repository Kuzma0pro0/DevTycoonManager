using UnityEditor;
using UnityEngine;

public class AddScriptNamespaceProcessor : UnityEditor.AssetModificationProcessor
{
    public const string DefaultGameNamespace = "DevIdle";
    public const string DefaultEditorNamespace = "Editor";

    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");

        var index = path.LastIndexOf(".");
        if (index < 0)
        {
            return;
        }

        var extension = path.Substring(index);
        if (extension != ".cs")
        {
            return;
        }

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;

        var content = System.IO.File.ReadAllText(path);
        var lastPart = path.Substring(path.IndexOf("Assets"));
        var namespaceString = lastPart.Substring(0, lastPart.LastIndexOf('/'));
        namespaceString = namespaceString.Replace("Assets/Source", DefaultGameNamespace).Replace("Assets/Editor", DefaultEditorNamespace).Replace('/', '.');
        content = content.Replace("#NAMESPACE#", namespaceString);
        System.IO.File.WriteAllText(path, content);

        AssetDatabase.Refresh();
    }
}


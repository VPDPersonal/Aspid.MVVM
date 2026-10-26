using System.IO; using UnityEngine; using UnityEditor.SceneManagement;
public static class OpenRecScene {
 public static object Main(){
  var proj=Path.GetDirectoryName(Application.dataPath);
  var p=File.ReadAllText(Path.Combine(proj,"Temp","rec-scene.txt")).Trim();
  EditorSceneManager.OpenScene(p,OpenSceneMode.Single); return p;
 }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ETModel;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ETEditor
{

    public class ScoreConfigEditor : EditorWindow
	{
        public partial class ScoreConfigCategory : ACategory<ScoreConfig>
        {
        }
        public class ScoreConfig : IConfig
        {
            public long Id { get; set; }
            //public int MinX { get; set; }
            //public int MaxX { get; set; }
            public double Y { get; set; }
            //public int MinZ { get; set; }
            //public int MaxZ { get; set; }
            public double X { get; set; }
            public double Z { get; set; }
        }
        [MenuItem("GameTools/配置豆子 =>ScoreConfig.txt")]
		public static void ShowWindow()
		{
			GetWindow(typeof(ScoreConfigEditor));
		}
        private long MaxID = 0;
        ScoreConfigCategory category;
        protected Dictionary<long, ScoreConfig> dict = new Dictionary<long, ScoreConfig>();
        public void Awake()
        {

            LoadConfig();
            SceneView.onSceneGUIDelegate += OnSceneFunc;
            //UnityEngine.SceneManagement.SceneManager.
        }
        private void OnGUI() 
		{
            if(GUILayout.Button("保存"))
            {
                SaveConfig();
            }
            //GUILayout.BeginScrollView()
            foreach (var kv in dict)
            {
                GUILayout.TextField($"{kv.Key} {kv.Value.X} {kv.Value.Y} {kv.Value.Z}"); 

            }
		}
        public void LoadConfig()
        {
            string path = Application.dataPath + "../../../Config/ScoreConfig.txt";
            string configStr = File.ReadAllText(path);
            foreach (string str in configStr.Split(new[] { "\n" }, System.StringSplitOptions.None))
            {
                try
                {
                    string str2 = str.Trim();
                    if (str2 == "")
                    {
                        continue;
                    }
                    ScoreConfig t = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<ScoreConfig>(str2); // MongoHelper.FromBson< ScoreConfig >(str2);
                    this.dict.Add(t.Id, t);
                    MaxID = Math.Max(t.Id, MaxID);
                }
                catch (Exception e)
                {
                    throw new Exception($"parser json fail: {str}", e);
                }
            }
        }
        public void SaveConfig()
        {
            var keyDocument = new MongoDB.Bson.BsonDocument();
            var keyWriter = new MongoDB.Bson.IO.BsonDocumentWriter(keyDocument);

            string path = Application.dataPath + "../../../Config/ScoreConfig.txt";
            string configStr = "";// File.ReadAllText(path);
            foreach (var c in dict)
            {
               configStr += $"{{\"_id\":NumberLong({c.Value.Id}),\"X\":{c.Value.X:0.00},\"Y\":{c.Value.Y:0.00},\"Z\":{c.Value.Z:0.00}}}\n";
            }
            //configStr = keyDocument.ToString();
            File.WriteAllText(path, configStr);
        }
        
        public void OnSceneFunc(SceneView sceneView)
        {
            
            if (Event.current.type == EventType.MouseDown
                && Event.current.button == 0)
            {
                RaycastHit hit;
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                UnityEngine.Debug.DrawRay(ray.origin, ray.direction, Color.blue, 10);
                if (Physics.Raycast(ray, out hit))
                {
                    ScoreConfig c = new ScoreConfig();
                    c.Id = ++MaxID;
                    c.X = hit.point.x;
                    c.Y = hit.point.y + 0.25f;
                    c.Z = hit.point.z;
                    dict.Add(c.Id, c);
                    UnityEngine.Debug.Log($"{hit.collider.gameObject} {hit.point}");
                    Repaint();
                }

                //UnityEngine.Debug.Log("Left-Mouse Down");
            }
        }


	}
}

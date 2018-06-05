using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cultivations;
using Grid;
using Node;
using TimeSystem;
using Tools;
using UnityEngine;

namespace Save
{
    public class SaveManager : Singleton<SaveManager>
    {
        public DateTime StopTime;

        private SaveInfo _saveInfo;
        private SaveNodes [,] _saveNodes;

        private string filenameNode = "nodes";
        private string extensionNode = "save";

        private string filenameTime = "time";
        private string extensionTime = "saveTime";

        [SerializeField]
       private Sprite[] _sprites;
        

        protected override void Awake()
        {
            base.Awake();
            LoadTime();
        
        }
        
        

        private void LoadTime()
        {
            _saveInfo = new SaveInfo(DateTime.Now, 1, 2018);
//            if (File.Exists(GetPath(filenameTime, extensionTime)))
//            {
//                _saveInfo = LoadFile<SaveInfo>(filenameTime, extensionTime);
//            }
//            else
//            {
//                
//            }
  
        }
        


        private void OnApplicationQuit()
        {
           SaveNodes();
           _saveInfo  = new SaveInfo(DateTime.Now, TimeManager.Instance.GetMonth(), TimeManager.Instance.GetYear());
           SaveFiles(_saveInfo, filenameTime, extensionTime);
        }


        private void SaveNodes()
        {
            int x = GridManager.Instance.GetNodeGrid().GetLength(0);
            int y = GridManager.Instance.GetNodeGrid().GetLength(1);
            _saveNodes = new SaveNodes[x,y];
            var tempGrid = GridManager.Instance.GetNodeGrid();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
  
                {
                    if (tempGrid[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Empty)
                    {
                        _saveNodes[i,j] = new SaveNodes(
                            tempGrid[i,j].GetListIndex(), 
                            tempGrid[i,j].GetComponent<NodeState>().CurrentState,
                            tempGrid[i,j].GetComponent<NodeState>().FieldType,
                            tempGrid[i,j].GetCultivationField()
                            );
                    }
                    else if(tempGrid[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.EmptyField 
                            || tempGrid[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Field)
                    {
                        _saveNodes[i,j] = new SaveNodes(
                            tempGrid[i,j].GetListIndex(), 
                            tempGrid[i,j].GetComponent<NodeState>().CurrentState,
                            tempGrid[i,j].GetComponent<NodeState>().FieldType,
                            tempGrid[i,j].GetCultivationField(),
                            tempGrid[i,j].GetComponent<PlantPrefab>().MyPlant
                        );
                    }
                    else if (tempGrid[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Farm)
                    {
                        
                             _saveNodes[i,j] = new SaveNodes(
                            tempGrid[i,j].GetListIndex(), 
                            tempGrid[i,j].GetComponent<NodeState>().CurrentState,
                            tempGrid[i,j].GetComponent<NodeState>().FieldType,
                            tempGrid[i,j].GetCultivationField(),
                            tempGrid[i,j].GetComponent<BuildingPrefab>().MyBuilding
                        );
                    }
      

                }
            }
            SaveFiles(_saveNodes, filenameNode, extensionNode);
        }


        public NodeBehaviour[,] LoadNodes(NodeBehaviour[,] nodes)
        {
            if (!File.Exists(GetPath(filenameNode, extensionNode))) return nodes;
            var loadedNodes = LoadFile<SaveNodes[,]>(filenameNode, extensionNode);
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    
                    nodes[i, j].GetComponent<NodeState>().CurrentState = loadedNodes[i, j].CurrentState;
                    nodes[i, j].GetComponent<NodeState>().FieldType = loadedNodes[i, j].FieldType;
                    nodes[i, j].SetCultivationListIndex(loadedNodes[i,j].ListIndex);
                    nodes[i, j].SetEmptyCultivationField(loadedNodes[i,j].EmptyCultivationField);
                    if (nodes[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.EmptyField
                        || nodes[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Field)
                    {
                        nodes[i, j].gameObject.AddComponent<PlantPrefab>();
                        nodes[i,j].GetComponent<PlantPrefab>().ChangeValues((Plant)loadedNodes[i,j].MyCultivation);
                        nodes[i, j].SetSprite(
                            _sprites[nodes[i, j].GetComponent<PlantPrefab>().MyPlant.SpriteIndex]);
                    }
                    else if (nodes[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Farm)
                    {
                        nodes[i, j].gameObject.AddComponent<BuildingPrefab>();
                        nodes[i,j].GetComponent<BuildingPrefab>().ChangeValues((Building)loadedNodes[i,j].MyCultivation);
                        nodes[i, j].SetSprite(
                            _sprites[nodes[i, j].GetComponent<BuildingPrefab>().MyBuilding.SpriteIndex]);
                    }
                    
                       
                    
            
                }
            }
            return nodes;
        }

        public DateTime GetStopTime()
        {
            return _saveInfo.StopTime;
        }

        public int GetSaveYear()
        {
            return _saveInfo.SaveYear;
        }

        public int GetSaveMonth()
        {
            return _saveInfo.SaveMonth;
        }

        public void SaveFiles<T>(T profile, string fileName, string extension)
        {
            File.WriteAllBytes(GetPath(fileName, extension), SerializeDate(profile));
        }

        public T LoadFile<T>(string filename, string extension)
        {
            byte[] data = File.ReadAllBytes(GetPath(filename, extension));
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return (T) binaryFormatter.Deserialize(ms);
        }

        private static byte[] SerializeDate<T>(T profile)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(ms, profile);
            return ms.ToArray();
        }


        private static string GetPath(string filename, string extension)
        {
            return Application.persistentDataPath + "/" + filename + "." +extension;
        }






    }
}

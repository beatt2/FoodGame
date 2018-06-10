using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cultivations;
using Grid;
using Money;
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

        private string filenameNode = "node";
        private string extensionNode = "save";

        private string filenameTime = "time";
        private string extensionTime = "saveTime";

        private bool _reset = false;

        [SerializeField]
       private Sprite[] _sprites;
        

        protected override void Awake()
        {
            base.Awake();
            LoadTime();
        
        }


        public void Reset()
        {
            File.Delete(GetPath(filenameNode,extensionNode));
            File.Delete(GetPath(filenameTime, extensionTime));
            _reset = true;
        }

        public Sprite[] GetSprites()
        {
            return _sprites;
        }
        
        

        private void LoadTime()
        {
           
            _saveInfo = new SaveInfo(DateTime.Now, 1, 2018, 500000, 0);
//            if (File.Exists(GetPath(filenameTime, extensionTime)))
//            {
//                _saveInfo = LoadFile<SaveInfo>(filenameTime, extensionTime);
//            }
//            else
//            {
//
//            }
  
        }

        public void SetHighestCultivationIndex(int value)
        {
            _saveInfo.HighestCultivationListIndex = value;
        }
        


        private void OnApplicationQuit()
        {
            if (_reset) return;
           SaveNodes();
           _saveInfo  = new SaveInfo(DateTime.Now, TimeManager.Instance.GetMonth(), TimeManager.Instance.GetYear(),SimpleMoneyManager.Instance.GetCurrentMoney(), _saveInfo.HighestCultivationListIndex);
           SaveFiles(_saveInfo, filenameTime, extensionTime);
        }
        public float GetMoney()
        {
            return _saveInfo.SaveMoney;
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
                        tempGrid[i, j].GetComponent<PlantPrefab>().MyPlant.BuildPrice = 0;
                        _saveNodes[i, j] = new SaveNodes(
                            tempGrid[i, j].GetListIndex(),
                            tempGrid[i, j].GetComponent<NodeState>().CurrentState,
                            tempGrid[i, j].GetComponent<NodeState>().FieldType,
                            tempGrid[i, j].GetCultivationField(),
                            tempGrid[i, j].GetComponent<PlantPrefab>().MyPlant,
                            tempGrid[i,j].GetNodeFence().Left,
                            tempGrid[i,j].GetNodeFence().LeftGameObject != null,
                            tempGrid[i,j].GetNodeFence().Right,
                            tempGrid[i,j].GetNodeFence().RightGameObject != null,
                            tempGrid[i,j].GetNodeFence().Up,
                            tempGrid[i,j].GetNodeFence().UpGameObject != null,
                            tempGrid[i,j].GetNodeFence().Down,
                            tempGrid[i,j].GetNodeFence().DownGameObject != null,
                            tempGrid[i,j].GetNodeFence().LeftSizeRank,
                            tempGrid[i,j].GetNodeFence().RightSizeRank,
                            tempGrid[i,j].GetNodeFence().UpSizeRank,
                            tempGrid[i,j].GetNodeFence().DownSizeRank
                            
                            
                                
                        );
                    }
                    else if (tempGrid[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Farm)
                    {
                            tempGrid[i, j].GetComponent<BuildingPrefab>().MyBuilding.BuildPrice = 0;
                             _saveNodes[i,j] = new SaveNodes(
                            tempGrid[i,j].GetListIndex(), 
                            tempGrid[i,j].GetComponent<NodeState>().CurrentState,
                            tempGrid[i,j].GetComponent<NodeState>().FieldType,
                            tempGrid[i,j].GetCultivationField(),
                            tempGrid[i,j].GetComponent<BuildingPrefab>().MyBuilding,
                            tempGrid[i,j].GetNodeFence().Left,
                            tempGrid[i,j].GetNodeFence().LeftGameObject != null,
                            tempGrid[i,j].GetNodeFence().Right,
                            tempGrid[i,j].GetNodeFence().RightGameObject != null,
                            tempGrid[i,j].GetNodeFence().Up,
                            tempGrid[i,j].GetNodeFence().UpGameObject != null,
                            tempGrid[i,j].GetNodeFence().Down,
                            tempGrid[i,j].GetNodeFence().DownGameObject != null,
                            tempGrid[i,j].GetNodeFence().LeftSizeRank,
                            tempGrid[i,j].GetNodeFence().RightSizeRank,
                            tempGrid[i,j].GetNodeFence().UpSizeRank,
                            tempGrid[i,j].GetNodeFence().DownSizeRank
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
            List<List<NodeBehaviour>> tempCultivationLocationList = new List<List<NodeBehaviour>>();
            for (int i = 0; i < _saveInfo.HighestCultivationListIndex +1; i++)
            {
                tempCultivationLocationList.Add(new List<NodeBehaviour>());
                
            }
           
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    
                    nodes[i, j].GetComponent<NodeState>().CurrentState = loadedNodes[i, j].CurrentState;
                    nodes[i, j].GetComponent<NodeState>().FieldType = loadedNodes[i, j].FieldType;
                    nodes[i, j].SetCultivationListIndex(loadedNodes[i,j].ListIndex);
                    nodes[i, j].SetEmptyCultivationField(loadedNodes[i,j].EmptyCultivationField);
                    nodes[i, j].GetNodeFence().Left = loadedNodes[i, j].FenceLeft;
                    nodes[i, j].GetNodeFence().Right = loadedNodes[i, j].FenceRight;
                    nodes[i, j].GetNodeFence().Up = loadedNodes[i, j].FenceUp;
                    nodes[i, j].GetNodeFence().Down = loadedNodes[i, j].FenceDown;

                    if (loadedNodes[i, j].FenceLeftOwner)
                    {
                        nodes[i, j].GetNodeFence().LeftGameObject = 
                            nodes[i, j].GetNodeFence().BuildFence(loadedNodes[i, j].SizeRankLeft > 2 ? 
                                GridManager.Instance.FenceOneBig :
                                GridManager.Instance.FenceOne, NodeFence.LeftLocation, 1);
                    }
                    if (loadedNodes[i, j].FenceRightOwner)
                    {
                        nodes[i, j].GetNodeFence().RightGameObject = 
                            nodes[i, j].GetNodeFence().BuildFence(loadedNodes[i, j].SizeRankRight > 2 ? 
                                GridManager.Instance.FenceOneBig :
                                GridManager.Instance.FenceOne, NodeFence.RightLocation, 0);
                    }
                    if (loadedNodes[i, j].FenceUpOwner)
                    {
                        nodes[i, j].GetNodeFence().UpGameObject = 
                            nodes[i, j].GetNodeFence().BuildFence(loadedNodes[i, j].SizeRankUp > 2 ? 
                                GridManager.Instance.FenceTwoBig :
                                GridManager.Instance.FenceTwo, NodeFence.UpLocation, -1);
                    }
                    if (loadedNodes[i, j].FenceDownOwner)
                    {
                        nodes[i, j].GetNodeFence().DownGameObject = 
                            nodes[i, j].GetNodeFence().BuildFence(loadedNodes[i, j].SizeRankDown > 2 ? 
                                GridManager.Instance.FenceTwoBig :
                                GridManager.Instance.FenceTwo, NodeFence.DownLocation, 1);
                    }
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
                        nodes[i, j].SetSprite(_sprites[nodes[i, j].GetComponent<BuildingPrefab>().MyBuilding.SpriteIndex]);
                    }

                    if (nodes[i, j].GetListIndex() != -1)
                    {
                        tempCultivationLocationList[nodes[i, j].GetListIndex()].Add(nodes[i, j]);
                    }
                       
                    
            
                }
            }
            GridManager.Instance.SetCultivationList(tempCultivationLocationList);
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

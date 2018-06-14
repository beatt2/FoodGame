using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Cultivations;
using Events;
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
        private SaveNodes[,] _saveNodes;

        private const string FilenameNode = "node";
        private const string ExtensionNode = "saveNode";

        private string _filenameReview = "review";
        private string _extensionReview = "saveReview";

        private string _filenameMessage = "messages";
        private string _extensionMessage = "saveMessage";

        private string filenameTime = "time";
        private string extensionTime = "saveTime";

        public Messages Messages;
        public Messages Reviews;

        private bool _reset = false;

        [SerializeField] private Sprite[] _sprites;


        protected override void Awake()
        {
            base.Awake();
            LoadTime();
            LoadMessagesAndReviews();
        }


        public void Reset()
        {
            File.Delete(GetPath(FilenameNode, ExtensionNode));
            File.Delete(GetPath(filenameTime, extensionTime));
            File.Delete(GetPath(_filenameMessage, _extensionMessage));
            File.Delete(GetPath(_filenameReview, _extensionReview));
            _reset = true;
        }

        public Sprite[] GetSprites()
        {
            return _sprites;
        }

        public Sprite GetSprite(int index)
        {
            return _sprites[index];
        }


        private void LoadTime()
        {
            if (File.Exists(GetPath(filenameTime, extensionTime)))
            {
                _saveInfo = LoadFile<SaveInfo>(filenameTime, extensionTime);
            }
            else
            {
                _saveInfo = new SaveInfo(DateTime.Now, 1, 2018, 5000, 0,
                    new Dictionary<NodeState.FieldTypeEnum, float>(), 0);
            }
        }

        public void SetHighestCultivationIndex(int value)
        {
            _saveInfo.HighestCultivationListIndex = value;
        }

        public Dictionary<NodeState.FieldTypeEnum, float> GetPercentageValues()
        {
            return _saveInfo.PercentageValues;
        }


        #if !UNITY_EDITOR
        private void OnApplicationPause(bool value)
        {
        #else
        private void OnApplicationQuit()
        {
        #endif    
            Debug.Log(SimpleMoneyManager.Instance.GetMoneyValueDict().Count);
            if(_reset) return;
            SaveNodes();
            SaveMessagesAndReviews();
            int tempTrack = 0;
            for (int i = 0; i < SimpleMoneyManager.Instance.GetMoneyValueDict().Count; i++)
            {
                for (int j = 0; j < SimpleMoneyManager.Instance.GetMoneyValueDict().ElementAt(i).Value.Count; j++)
                {
                    tempTrack++;
                }
            }
            _saveInfo = new SaveInfo
            (
                DateTime.Now,
                TimeManager.Instance.GetMonth(),
                TimeManager.Instance.GetYear(),
                SimpleMoneyManager.Instance.GetCurrentMoney(),
                _saveInfo.HighestCultivationListIndex,
                SimpleMoneyManager.Instance.GetPercentageValues(),
                tempTrack
            );


            SaveFiles(_saveInfo, filenameTime, extensionTime);
        }

        public float GetMoney()
        {
            return _saveInfo.SaveMoney;
        }

        public int GetMoneyValueCount()
        {
            return _saveInfo.TotalAmountOfMoneyValues;
        }

        private void SaveMessagesAndReviews()
        {
            var tempMessagesSave = Messages.GetInboxInt();
            var tempReviewsSave = Reviews.GetInboxInt();
            SaveFiles(tempMessagesSave, _filenameMessage, _extensionMessage);
            SaveFiles(tempReviewsSave, _filenameReview, _extensionReview);
        }

        private void LoadMessagesAndReviews()
        {
            if (File.Exists(GetPath(_filenameMessage, _extensionMessage)))
            {
                var tempMessages = LoadFile<List<int>>(_filenameMessage, _extensionMessage);
                Messages.SetInboxInt(tempMessages);
            }

            if (File.Exists(GetPath(_filenameReview, _extensionReview)))
            {
                var tempReviews = LoadFile<List<int>>(_filenameReview, _extensionReview);
                Reviews.SetInboxInt(tempReviews);
            }
        }


        //REFACTOR
        private void SaveNodes()
        {
            int x = GridManager.Instance.GetNodeGrid().GetLength(0);
            int y = GridManager.Instance.GetNodeGrid().GetLength(1);
            _saveNodes = new SaveNodes[x, y];
            var tempGrid = GridManager.Instance.GetNodeGrid();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)

                {
                    if (tempGrid[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Empty)
                    {
                        _saveNodes[i, j] = new SaveNodes(
                            tempGrid[i, j].GetListIndex(),
                            tempGrid[i, j].GetComponent<NodeState>().CurrentState,
                            tempGrid[i, j].GetComponent<NodeState>().FieldType,
                            tempGrid[i, j].GetCultivationField()
                        );
                    }
                    else if (tempGrid[i, j].GetComponent<NodeState>().CurrentState ==
                             NodeState.CurrentStateEnum.EmptyField
                             || tempGrid[i, j].GetComponent<NodeState>().CurrentState ==
                             NodeState.CurrentStateEnum.Field)
                    {
                        tempGrid[i, j].GetComponent<PlantPrefab>().MyPlant.BuildPrice = 0;
                        tempGrid[i, j].GetComponent<PlantPrefab>().FirstRun = true;
                        _saveNodes[i, j] = new SaveNodes(
                            tempGrid[i, j].GetListIndex(),
                            tempGrid[i, j].GetComponent<NodeState>().CurrentState,
                            tempGrid[i, j].GetComponent<NodeState>().FieldType,
                            tempGrid[i, j].GetCultivationField(),
                            tempGrid[i, j].GetComponent<PlantPrefab>().MyPlant,
                            tempGrid[i, j].GetNodeFence().Left,
                            tempGrid[i, j].GetNodeFence().LeftGameObject != null,
                            tempGrid[i, j].GetNodeFence().Right,
                            tempGrid[i, j].GetNodeFence().RightGameObject != null,
                            tempGrid[i, j].GetNodeFence().Up,
                            tempGrid[i, j].GetNodeFence().UpGameObject != null,
                            tempGrid[i, j].GetNodeFence().Down,
                            tempGrid[i, j].GetNodeFence().DownGameObject != null,
                            tempGrid[i, j].GetNodeFence().LeftSizeRank,
                            tempGrid[i, j].GetNodeFence().RightSizeRank,
                            tempGrid[i, j].GetNodeFence().UpSizeRank,
                            tempGrid[i, j].GetNodeFence().DownSizeRank,
                            tempGrid[i, j].GetComponent<PlantPrefab>().GetSavedPlant()
                        );
                    }
                    else if (tempGrid[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Farm)
                    {
                        tempGrid[i, j].GetComponent<BuildingPrefab>().MyBuilding.BuildPrice = 0;
                        tempGrid[i, j].GetComponent<BuildingPrefab>().FirstRun = true;
                        _saveNodes[i, j] = new SaveNodes(
                            tempGrid[i, j].GetListIndex(),
                            tempGrid[i, j].GetComponent<NodeState>().CurrentState,
                            tempGrid[i, j].GetComponent<NodeState>().FieldType,
                            tempGrid[i, j].GetCultivationField(),
                            tempGrid[i, j].GetComponent<BuildingPrefab>().MyBuilding,
                            tempGrid[i, j].GetNodeFence().Left,
                            tempGrid[i, j].GetNodeFence().LeftGameObject != null,
                            tempGrid[i, j].GetNodeFence().Right,
                            tempGrid[i, j].GetNodeFence().RightGameObject != null,
                            tempGrid[i, j].GetNodeFence().Up,
                            tempGrid[i, j].GetNodeFence().UpGameObject != null,
                            tempGrid[i, j].GetNodeFence().Down,
                            tempGrid[i, j].GetNodeFence().DownGameObject != null,
                            tempGrid[i, j].GetNodeFence().LeftSizeRank,
                            tempGrid[i, j].GetNodeFence().RightSizeRank,
                            tempGrid[i, j].GetNodeFence().UpSizeRank,
                            tempGrid[i, j].GetNodeFence().DownSizeRank,
                            tempGrid[i, j].GetComponent<BuildingPrefab>().GetSavedBuilding()
                        );
                    }
                }
            }

            SaveFiles(_saveNodes, FilenameNode, ExtensionNode);
        }

        //REFACTOR
        public NodeBehaviour[,] LoadNodes(NodeBehaviour[,] nodes)
        {
            if (!File.Exists(GetPath(FilenameNode, ExtensionNode))) return nodes;
            var loadedNodes = LoadFile<SaveNodes[,]>(FilenameNode, ExtensionNode);
            List<List<NodeBehaviour>> tempCultivationLocationList = new List<List<NodeBehaviour>>();
            for (int i = 0; i < _saveInfo.HighestCultivationListIndex + 1; i++)
            {
                tempCultivationLocationList.Add(new List<NodeBehaviour>());
            }

            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    nodes[i, j].GetComponent<NodeState>().CurrentState = loadedNodes[i, j].CurrentState;
                    nodes[i, j].GetComponent<NodeState>().FieldType = loadedNodes[i, j].FieldType;
                    nodes[i, j].SetCultivationListIndex(loadedNodes[i, j].ListIndex);
                    nodes[i, j].SetEmptyCultivationField(loadedNodes[i, j].EmptyCultivationField);
                    nodes[i, j].GetNodeFence().Left = loadedNodes[i, j].FenceLeft;
                    nodes[i, j].GetNodeFence().Right = loadedNodes[i, j].FenceRight;
                    nodes[i, j].GetNodeFence().Up = loadedNodes[i, j].FenceUp;
                    nodes[i, j].GetNodeFence().Down = loadedNodes[i, j].FenceDown;

                    if (loadedNodes[i, j].FenceLeftOwner)
                    {
                        nodes[i, j].GetNodeFence().LeftGameObject =
                            nodes[i, j].GetNodeFence()
                                .BuildFence(
                                    loadedNodes[i, j].SizeRankLeft > 2
                                        ? GridManager.Instance.FenceOneBig
                                        : GridManager.Instance.FenceOne, NodeFence.LeftLocation, 1);
                    }

                    if (loadedNodes[i, j].FenceRightOwner)
                    {
                        nodes[i, j].GetNodeFence().RightGameObject =
                            nodes[i, j].GetNodeFence()
                                .BuildFence(
                                    loadedNodes[i, j].SizeRankRight > 2
                                        ? GridManager.Instance.FenceOneBig
                                        : GridManager.Instance.FenceOne, NodeFence.RightLocation, 0);
                    }

                    if (loadedNodes[i, j].FenceUpOwner)
                    {
                        nodes[i, j].GetNodeFence().UpGameObject =
                            nodes[i, j].GetNodeFence()
                                .BuildFence(
                                    loadedNodes[i, j].SizeRankUp > 2
                                        ? GridManager.Instance.FenceTwoBig
                                        : GridManager.Instance.FenceTwo, NodeFence.UpLocation, -1);
                    }

                    if (loadedNodes[i, j].FenceDownOwner)
                    {
                        nodes[i, j].GetNodeFence().DownGameObject =
                            nodes[i, j].GetNodeFence()
                                .BuildFence(
                                    loadedNodes[i, j].SizeRankDown > 2
                                        ? GridManager.Instance.FenceTwoBig
                                        : GridManager.Instance.FenceTwo, NodeFence.DownLocation, 1);
                    }

                    if (nodes[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.EmptyField
                        || nodes[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Field)
                    {
                        nodes[i, j].gameObject.AddComponent<PlantPrefab>();
                        nodes[i, j].GetComponent<PlantPrefab>().ChangeValues((Plant) loadedNodes[i, j].MyCultivation);
                        nodes[i, j].SetSprite(_sprites[nodes[i, j].GetComponent<PlantPrefab>().MyPlant.SpriteIndex]);
                        if ((Plant) loadedNodes[i, j].MySavedCultivation != null)
                        {
                            nodes[i, j].GetComponent<PlantPrefab>()
                                .SetSavedPlant((Plant) loadedNodes[i, j].MySavedCultivation);
                        }
                    }
                    else if (nodes[i, j].GetComponent<NodeState>().CurrentState == NodeState.CurrentStateEnum.Farm)
                    {
                        nodes[i, j].gameObject.AddComponent<BuildingPrefab>();
                        nodes[i, j].GetComponent<BuildingPrefab>()
                            .ChangeValues((Building) loadedNodes[i, j].MyCultivation);
                        nodes[i, j].SetSprite(
                            _sprites[nodes[i, j].GetComponent<BuildingPrefab>().MyBuilding.SpriteIndex]);
                        if ((Building) loadedNodes[i, j].MySavedCultivation != null)
                        {
                            nodes[i, j].GetComponent<BuildingPrefab>()
                                .SetSavedBuilding((Building) loadedNodes[i, j].MySavedCultivation);
                        }
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
            return Application.persistentDataPath + "/" + filename + "." + extension;
        }
    }
}
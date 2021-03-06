﻿/*
 * Copyright (c) 2018, Jeffrey van den Elshout
 * All rights reserved.
 *
    * Redistribution and use in source and binary forms, with or without
    * modification, are permitted provided that the following conditions are met:
    *
    * 1. Redistributions of source code must retain the above copyright notice, this
    *    list of conditions and the following disclaimer.
    * 2. Redistributions in binary form must reproduce the above copyright notice,
    *    this list of conditions and the following disclaimer in the documentation
    *    and/or other materials provided with the distribution.
    *
    * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
    * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
    * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
    * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
    * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
    * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
    * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
    * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
    * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    */
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
using Notifications;
using TimeSystem;
using Tools;
using UnityEngine;




namespace Save
{
    public class SaveManager : Singleton<SaveManager>
    {


        private SaveInfo _saveInfo;
        private SaveNodes[,] _saveNodes;

        private const string FilenameNode = "node";
        private const string ExtensionNode = "saveNode";

        private const string FilenameReview = "review";
        private const string ExtensionReview = "saveReview";

        private const string FilenameMessage = "messages";
        private const string ExtensionMessage = "saveMessage";

        private const string FilenameTime = "time";
        private const string ExtensionTime = "saveTime";

        public Messages Messages;
        public ReviewMessages Reviews;


        [SerializeField] private Sprite[] _sprites;


        protected override void Awake()
        {
            base.Awake();
            LoadTime();

        }


        public void Reset()
        {
            File.Delete(GetPath(FilenameNode, ExtensionNode));
            File.Delete(GetPath(FilenameTime, ExtensionTime));
            File.Delete(GetPath(FilenameMessage, ExtensionMessage));
            File.Delete(GetPath(FilenameReview, ExtensionReview));
        }

        public Sprite GetSprite(int index)
        {
            return _sprites[index];
        }


        private void LoadTime()
        {
            if (File.Exists(GetPath(FilenameTime, ExtensionTime)))
            {
                _saveInfo = LoadFile<SaveInfo>(FilenameTime, ExtensionTime);
            }
            else
            {
                _saveInfo = new SaveInfo
                (
                    DateTime.Now,
                    1,
                    2018,
                    5000,
                    new List<int>(),
                    new Dictionary<NodeState.FieldTypeEnum, float>(),
                    0,
                    false,
                    new Dictionary<int, bool>(),
                    new Dictionary<int, bool>(),
                    TimeManager.Instance.GetWaitForSeconds()

                );


            }


        }

        public int GetWaitForSeconds()
        {
            return _saveInfo.WaitForSeconds;
        }

        public void SetCultivationIndexList(int value)
        {
            _saveInfo.DictionaryIndexEntrys.Add(value);
        }

        public Dictionary<NodeState.FieldTypeEnum, float> GetPercentageValues()
        {
            return _saveInfo.PercentageValues;
        }

        public void SetTutorialBool(bool value)
        {
            if (value)
            {
                MyNotificationManager.SetTutorialNotifications();
            }
            _saveInfo.TutorialHasPlayed = value;
        }

        public bool GetTutorialBool()
        {
            return _saveInfo.TutorialHasPlayed;
        }
        private void OnApplicationFocus(bool hasFocus)
        {
            //OnApplicationChange(!hasFocus);

        }

        private void OnApplicationPause(bool value)
        {
            OnApplicationChange(value);
        }

        private void OnApplicationQuit()
        {
            OnApplicationChange(true);
        }

        public void OnApplicationChange(bool value)
        {

            if (value)
            {

                SaveMiscFiles();
                SaveNodes();
                SaveMessagesAndReviews();
                


            }
            else
            {
                
                LoadTime();
                //SimpleMoneyManager.Instance.Start();
                TimeManager.Instance.Start();
            }
        }

        private void SaveMiscFiles()
        {
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
                _saveInfo.DictionaryIndexEntrys,
                SimpleMoneyManager.Instance.GetPercentageValues(),
                tempTrack,
                _saveInfo.TutorialHasPlayed,
                Reviews.GetReadDict(),
                Messages.GetReadDict(),
                TimeManager.Instance.GetWaitForSeconds()

            );
            SaveFiles(_saveInfo, FilenameTime, ExtensionTime);
        }

        public float GetMoney()
        {
            return _saveInfo.SaveMoney;
        }

        public int GetMoneyValueCount()
        {
            return _saveInfo.TotalAmountOfMoneyValues;
        }

//        public List<CultivationPrefabList> GetActiveCultivationPrefabLists()
//        {
//            return _saveInfo.ActiveCultivationPrefabLists;
//        }

        private void SaveMessagesAndReviews()
        {
            var tempMessagesSave = Messages.GetInboxInt();
            var tempReviewsSave = Reviews.GetInboxInt();
            SaveFiles(tempMessagesSave, FilenameMessage, ExtensionMessage);
            SaveFiles(tempReviewsSave, FilenameReview, ExtensionReview);
        }

        public void LoadMessagesAndReviews()
        {
            if (File.Exists(GetPath(FilenameMessage, ExtensionMessage)))
            {
                var tempMessages = LoadFile<List<int>>(FilenameMessage, ExtensionMessage);
                Messages.SetReadDictionary(_saveInfo.ReadDictionaryMessages);

                Messages.SetInboxInt(tempMessages);

            }

            if (File.Exists(GetPath(FilenameReview, ExtensionReview)))
            {
                var tempReviews = LoadFile<List<int>>(FilenameReview, ExtensionReview);
                Reviews.SetReadDictionary(_saveInfo.ReadDictionaryReview);
                Reviews.SetInboxInt(tempReviews);

            }
        }


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
                        _saveNodes[i, j] = new SaveNodes
                        (
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


        public NodeBehaviour[,] LoadNodes(NodeBehaviour[,] nodes)
        {
            if (!File.Exists(GetPath(FilenameNode, ExtensionNode))) return nodes;
            var loadedNodes = LoadFile<SaveNodes[,]>(FilenameNode, ExtensionNode);
            Dictionary<int,List<NodeBehaviour>> tempCultivationLocationDict = new Dictionary<int, List<NodeBehaviour>>();
            for (int i = 0; i < _saveInfo.DictionaryIndexEntrys.Count; i++)
            {
                tempCultivationLocationDict.Add(_saveInfo.DictionaryIndexEntrys[i],new List<NodeBehaviour>());
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
                            nodes[i, j].GetNodeFence().BuildFence(
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
                        nodes[i, j].GetComponent<PlantPrefab>().FirstRun = true;
                        nodes[i, j].GetComponent<PlantPrefab>().ChangeValues((Plant)loadedNodes[i, j].MyCultivation);
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
                        nodes[i, j].GetComponent<BuildingPrefab>().FirstRun = true;
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
                        tempCultivationLocationDict[nodes[i, j].GetListIndex()].Add(nodes[i, j]);
                    }
                }
            }

            GridManager.Instance.SetCultivationDictionary(tempCultivationLocationDict);
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

        private static void SaveFiles<T>(T profile, string fileName, string extension)
        {
            File.WriteAllBytes(GetPath(fileName, extension), SerializeDate(profile));
        }

        private static T LoadFile<T>(string filename, string extension)
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

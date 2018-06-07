﻿using System;
using System.Security.Policy;
using Cultivations;
using Grid;
using Money;
using Node;
using Save;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SidePanel : MonoBehaviour
    {
        public Text HeaderText;
        public Text UpgradeText;
        public Text MoneyTickText;


        private PanelLerp _panelLerp;
        private Sprite _defaultSprite;
        public Image MyImage;

        public GameObject DestroyPanel;
        public GameObject UpgradePanel;

        public Button KillButton;
        public Button UpgradeButton;


        private Cultivation _currentCultivation;

        private void Awake()
        {
            _defaultSprite = MyImage.sprite;
            _panelLerp = GetComponent<PanelLerp>();
        }

        private void Update()
        {
            if (!SidePanelActive() || _currentCultivation == null) return;
            if (SimpleMoneyManager.Instance.GetCurrentMoney() > _currentCultivation.BuildPrice)
            {
                UpgradeButton.interactable = true;
            }
            else
            {
                UpgradeButton.interactable = false;
            }
        }

        public void TogglePannel()
        {
            _panelLerp.ToggleLerp();
        }

        public bool SidePanelActive()
        {
            return _panelLerp.IsActive();
        }


        public void SetPanel(Cultivation cultivation)
        {
            if (!_panelLerp.IsActive())
            {
                _panelLerp.ToggleLerp();
            }

            _currentCultivation = cultivation;
            HeaderText.text = cultivation.Name + " " + cultivation.FieldType;
            MyImage.sprite = SaveManager.Instance.GetSprites()[cultivation.SpriteIndex];
            
         
            UpgradeText.text = cultivation.UpgradeValue.ToString();
            MoneyTickText.text = "money per month" + cultivation.MoneyTick;

            UpgradeButton.interactable = cultivation.MyCultivationState != NodeState.CurrentStateEnum.Field;
            KillButton.interactable = cultivation.MyCultivationState != NodeState.CurrentStateEnum.EmptyField;
        }

        public void OnUpgradeButtonPressed()
        {
            switch (_currentCultivation.MyCultivationState)
            {
                case NodeState.CurrentStateEnum.EmptyField:
                    switch (_currentCultivation.FieldType)
                    {
                        case NodeState.FieldTypeEnum.Carrot:
                            GridManager.Instance.BuildingPlacement.BuildField(0);
                            break;
                        case NodeState.FieldTypeEnum.Corn:
                            GridManager.Instance.BuildingPlacement.BuildField(1);
                            break;
                        case NodeState.FieldTypeEnum.Blackberries:
                            GridManager.Instance.BuildingPlacement.BuildField(2);
                            break;
                        case NodeState.FieldTypeEnum.Apple:
                            GridManager.Instance.BuildingPlacement.BuildField(4);
                            break;
                        case NodeState.FieldTypeEnum.Tomato:
                            GridManager.Instance.BuildingPlacement.BuildField(3);
                            break;

                        case NodeState.FieldTypeEnum.Nothing:
                            break;
                        case NodeState.FieldTypeEnum.Tree:
                            GridManager.Instance.BuildingPlacement.BuildField(4);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case NodeState.CurrentStateEnum.Farm:
                    UpgradePanel.SetActive(true);
                    //UpgradePanel.GetComponent<UpgradeTab>().ActivateTab(GridManager.Instance.GetSelectedNode().GetComponent<BuildingPrefab>().MyBuilding);
                    break;
                case NodeState.CurrentStateEnum.Field:
                    throw new NotImplementedException();


                case NodeState.CurrentStateEnum.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnKillButtonPressed()
        {
            DestroyPanel.SetActive(true);
        }

        public void OnKillDeny()
        {
            DestroyPanel.SetActive(false);
        }

        public void OnKillConfirm()
        {
            GridManager.Instance.BuildingPlacement.RemoveTiles(GridManager.Instance.GetSelectedNode());
            DestroyPanel.SetActive(false);
        }
    }
}

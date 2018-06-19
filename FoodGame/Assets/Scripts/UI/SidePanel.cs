﻿using System;
using System.Security.Policy;
using Cultivations;
using Grid;
using Money;
using Node;
using Save;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SidePanel : MonoBehaviour
    {
        public Text HeaderText;

        public Text UpgradeText;
        public Text MoneyTickText;
        public Text ExpenseTickText;
        public Text HappinessText;
        public Text EnviromentalText;


        private PanelLerp _panelLerp;
        private Sprite _defaultSprite;
        public Image MyImage;

        public GameObject DestroyPanel;
        public GameObject UpgradePanel;

        public Button KillButton;
        public Button UpgradeButton;

        public Sprite[] SidePanelIcons;

        public TutorialScript TutorialScript;


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
            TutorialScript.OnClickField();
            _currentCultivation = cultivation;
            HeaderText.text = cultivation.Name;
            MyImage.sprite = SidePanelIcons[cultivation.SidePanelSpriteIndex];
            if (!cultivation.Upgrade)
            {
                UpgradeText.text = "Upgrade value = " + cultivation.UpgradeValue;
            }
            else
            {
                UpgradeText.text = "";
            }

            MoneyTickText.text = "Income: " + cultivation.MoneyTick;
            ExpenseTickText.text = "Expense: " + cultivation.ExpenseTick;
            HappinessText.text = "Happiness: " + cultivation.Happiness;
            EnviromentalText.text = "Enviromental: " + cultivation.EnviromentValue;
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
                            switch (_currentCultivation.SizeRank)
                            {
                                    case 1:
                                        GridManager.Instance.BuildingPlacement.BuildField(9);
                                        break;
                                    case 2:
                                        GridManager.Instance.BuildingPlacement.BuildField(11);
                                        break;
                                    case 3:
                                        GridManager.Instance.BuildingPlacement.BuildField(10);
                                        break;
                            }
             
                            break;
                        case NodeState.FieldTypeEnum.Corn:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    GridManager.Instance.BuildingPlacement.BuildField(6);
                                    break;
                                case 2:
                                    GridManager.Instance.BuildingPlacement.BuildField(8);
                                    break;
                                case 3:
                                    GridManager.Instance.BuildingPlacement.BuildField(7);
                                    break;
                            }
                            break;
                        case NodeState.FieldTypeEnum.Blackberries:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    GridManager.Instance.BuildingPlacement.BuildField(3);
                                    break;
                                case 2:
                                    GridManager.Instance.BuildingPlacement.BuildField(5);
                                    break;
                                case 3:
                                    GridManager.Instance.BuildingPlacement.BuildField(4);
                                    break;
                            }
                            break;
                        case NodeState.FieldTypeEnum.Apple:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    GridManager.Instance.BuildingPlacement.BuildField(0);
                                    break;
                                case 2:
                                    GridManager.Instance.BuildingPlacement.BuildField(2);
                                    break;
                                case 3:
                                    GridManager.Instance.BuildingPlacement.BuildField(1);
                                    break;
                            }
                            
                            break;
                        case NodeState.FieldTypeEnum.Tomato:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    GridManager.Instance.BuildingPlacement.BuildField(12);
                                    break;
                                case 2:
                                    GridManager.Instance.BuildingPlacement.BuildField(14);
                                    break;
                                case 3:
                                    GridManager.Instance.BuildingPlacement.BuildField(13);
                                    break;
                            }
                            break;
                        case NodeState.FieldTypeEnum.Nothing:
                            break;
                        case NodeState.FieldTypeEnum.Grapes:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    GridManager.Instance.BuildingPlacement.BuildField(15);
                                    break;
                                case 2:
                                    GridManager.Instance.BuildingPlacement.BuildField(17);
                                    break;
                                case 3:
                                    GridManager.Instance.BuildingPlacement.BuildField(16);
                                    break;
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case NodeState.CurrentStateEnum.Farm:
                    if (!_currentCultivation.Upgrade)
                    {
                        GridManager.Instance.BuildingPlacement.UpgradeFarm(_currentCultivation.UpgradePrefabIndex);
                        TutorialScript.OnUpgrade();
                    }
                    
                    else
                    {
                        Debug.Log("Already upgraded");
                    }
                    break;
                case NodeState.CurrentStateEnum.Field:
                    if (!_currentCultivation.Upgrade)
                    {
                        GridManager.Instance.BuildingPlacement.UpgradeField(_currentCultivation.UpgradePrefabIndex);
                        TutorialScript.OnUpgrade();

                    }
                    else
                    {
                        Debug.Log("Already upgraded");
                    }
                    break;
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

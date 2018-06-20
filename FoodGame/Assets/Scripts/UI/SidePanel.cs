using System;
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
                UpgradeText.text = "Upgrade prijs = " + cultivation.UpgradeValue;
            }
            else
            {
                UpgradeText.text = "";
            }

            if (cultivation.MyCultivationState == NodeState.CurrentStateEnum.EmptyField)
            {
                UpgradeText.text = "Upgrade prijs = " + UglyHiddenBoi();
            }

            MoneyTickText.text = "Inkomsten: " + cultivation.MoneyTick;
            ExpenseTickText.text = "Kosten: " + cultivation.ExpenseTick;
            HappinessText.text = "Opinie: " + cultivation.Happiness;
            EnviromentalText.text = "Biologisch: " + cultivation.EnviromentValue;
            //UpgradeButton.interactable = cultivation.MyCultivationState != NodeState.CurrentStateEnum.Field;
            KillButton.interactable = cultivation.MyCultivationState != NodeState.CurrentStateEnum.EmptyField;
        }

        private int UglyHiddenBoi()
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
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(9);
                                case 2:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(11);
                                case 3:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(10);
                            }

                            break;
                        case NodeState.FieldTypeEnum.Corn:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(6);
                                case 2:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(8);
                                case 3:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(7);
                            }

                            break;
                        case NodeState.FieldTypeEnum.Blackberries:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(3);
                                case 2:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(5);
                                case 3:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(4);
                            }

                            break;
                        case NodeState.FieldTypeEnum.Apple:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(0);
                                case 2:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(2);
                                case 3:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(1);
                            }

                            break;
                        case NodeState.FieldTypeEnum.Tomato:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(12);
                                case 2:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(14);
                                case 3:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(13);
                            }

                            break;
                        case NodeState.FieldTypeEnum.Nothing:
                            break;
                        case NodeState.FieldTypeEnum.Grapes:
                            switch (_currentCultivation.SizeRank)
                            {
                                case 1:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(15);
                                case 2:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(17);
                                case 3:
                                    return GridManager.Instance.BuildingPlacement.GetFieldBuildPrice(16);
                            }

                            break;
                    }

                    break;
            }

            return 0;
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

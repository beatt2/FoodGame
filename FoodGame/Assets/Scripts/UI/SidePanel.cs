using System;
using Cultivations;
using Grid;
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

        private Cultivation _currentCultivation;

        private void Awake()
        {
            _defaultSprite = MyImage.sprite;
            _panelLerp = GetComponent<PanelLerp>();
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
            MyImage.sprite = cultivation.Image;
 
            //TODO make a UpgradeCost variable in Cultivation
            UpgradeText.text = cultivation.UpgradeValue.ToString();
            MoneyTickText.text = cultivation.MoneyTick.ToString();

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
                        case NodeState.FieldTypeEnum.Nothing:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case NodeState.CurrentStateEnum.Farm:
                    throw new NotImplementedException();
                case NodeState.CurrentStateEnum.Field:
                    throw new NotImplementedException();


                case NodeState.CurrentStateEnum.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
       


    }
}

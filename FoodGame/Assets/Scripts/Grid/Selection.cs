﻿using Cultivations;
using UI;
using UnityEngine;

namespace Grid
{
   public class Selection : MonoBehaviour
   {
      public SidePanel MySidePanel;
      public GameObject BuildPanel;
      public GameObject YesNoButtons;
      public GameObject YesNoBuildButton;
      public BuildingTab MyBuildingTab;

      private int _index;



      public void ToggleBuildPanel(bool value)
      {
         BuildPanel.SetActive(value);
         MyBuildingTab.BuildingTabActive = true;

      }

      //Called from button
      public void BuildFarm(int index)
      {
         _index = index;
         SetManager();
      }


      public void ConfirmLocationPressed(bool value)
      {
         if (!GridManager.Instance.ConfirmLocation(value)) return;
         ToggleYesNo(false);
         ToggleYesNoBuildButton(value);
         SetYesNoBuildLocation(Camera.main.WorldToScreenPoint(GridManager.Instance.GetSelectedNode().transform.position));


      }

      public void ConfirmBuildLocationPressed(bool value)
      {
         if (value)
         {
            GridManager.Instance.ConfirmBuildFarmButtonPressed(_index);

         }
         else
         {
            GridManager.Instance.CancelBuildState();
         }
         ToggleYesNoBuildButton(false);

      }

      private void SetManager()
      {

         if (GridManager.Instance.GetSelectedNode() != null)
         {
            GridManager.Instance.SetSelectionSize(4);
            ToggleYesNo(true);
         }
         ToggleBuildPanel(false);
      }

      public void ToggleYesNo(bool value)
      {
         YesNoButtons.SetActive(value);
      }


      public bool YesNoActive()
      {
         return YesNoButtons.activeSelf;
      }

      public void SetYesNoLocation(Vector3 position)
      {
         YesNoButtons.transform.position = position;
      }

      public void SetYesNoBuildLocation(Vector3 position)
      {
         YesNoBuildButton.transform.position = position;
      }


      private void ToggleYesNoBuildButton(bool value)
      {
         YesNoBuildButton.SetActive(value);
      }

      public bool YesNoBuildActive()
      {
         return YesNoBuildButton.activeSelf;
      }

      public void SetSidePanel(Cultivation cultivation)
      {
         MySidePanel.SetPanel(cultivation);
      }

      public void ToggleSidePanel()
      {
         MySidePanel.TogglePannel();
      }

      public bool SidePannelActive()
      {
         return MySidePanel.SidePanelActive();
      }



   }
}

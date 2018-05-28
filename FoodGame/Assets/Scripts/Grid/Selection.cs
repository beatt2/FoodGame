﻿using System.Security.Cryptography.X509Certificates;
using Cultivations;
using Grid;
using UI;
using UnityEngine;

public class Selection : MonoBehaviour
{
   public SidePanel MySidePanel;
   public GameObject BuildPanel;
   public GameObject YesNoButtons;
   public GameObject YesNoBuildButton;

   private int _index;




   public void ToggleBuildPanel(bool value)
   {
      BuildPanel.SetActive(value);
   }

   //Called from button
   public void BuildFarm(int index)
   {   
      _index = index;
      SetManager();
   }

   public void ConfirmLocationPressed(bool value)
   {
      GridManager.Instance.ConfirmLocation(value);
      ToggleYesNo(false);
      ToggleYesNoBuildButton(value);
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
      GridManager.Instance.SetSelectionSize();
      if (GridManager.Instance.GetSelectedNode() != null)
      {
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
   
   public void ToggleYesNoBuildButton(bool value)
   {
      YesNoBuildButton.SetActive(value);
   }
   
   public bool YesNoBuildActive()
   {
      return YesNoBuildButton.activeSelf;
   }

   public void SetSidePanel(Cultivation cultivation)
   {
      MySidePanel.SetPanel(cultivation)  ;
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
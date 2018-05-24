using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Selection : MonoBehaviour
{
   public PanelLerp LerpPanel;

   public void LerpToTarget()
   {
      StartCoroutine(LerpPanel.LerpToTarget());
   }

   public void LerpBackFromTarget()
   {
      StartCoroutine(LerpPanel.LerpBackFromTarget());
   }

   public void BuildCornFarm()
   {
      
   }

   public void BuildCarrotFarm()
   {
      
   }

   public void BuildglassHouse()
   {
      
   }
   

}

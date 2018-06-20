using Node;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
	public class Reviewprefab : MonoBehaviour
	{
		public Text Headline;
		public Text Effect;
		public Text Content;




		public void ChangeText(string headline,string preInsert,string effect)
		{
			Headline.text = headline;
			Content.text = preInsert;
			Effect.text = effect;

		}

		public void ChangeText(string headline,string preInsert,NodeState.FieldTypeEnum fieldTypeEnum, string afterInsert,string effect)
		{
			//Headline.text = headline;
			Content.text = preInsert + " " +  GetInsert(fieldTypeEnum) + " " + afterInsert;
			Effect.text = effect;
		}

		private string GetInsert(NodeState.FieldTypeEnum fieldType)
		{
			switch (fieldType)
			{
				case NodeState.FieldTypeEnum.Apple:
					return "appel";
				case NodeState.FieldTypeEnum.Blackberries:
					return "braam";
				case NodeState.FieldTypeEnum.Carrot:
					return "wortel";
				case NodeState.FieldTypeEnum.Corn:
					return "mais";
				case NodeState.FieldTypeEnum.Grapes:
					return "druif";
				case NodeState.FieldTypeEnum.Tomato:
					return "tomaat";
			}

			return "";
		}

	}
}


using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.FlowNodes
{
	[CrogenRegisterScript, NodePath("Flow")]
	public class GotoNodeSO : NodeSO
	{
		[field: SerializeField] public NodeSO Destination { get; private set; }

		public override string GetNodeName() => "Goto";
		public override string GetTooltipText() => $"{Destination?.name}���� �����ŵ�ϴ�.";

		public bool SelectFilter()
		{
			return true;
		}

		public override bool IsWarning() => Destination == null;
		public override string GetWarningText() => "Destination�� null�Դϴ�.";

		public override bool IsError() => Destination != null && Destination.StorySO != this.StorySO;
		public override string GetErrorText() => $"\'{Destination.name}\'��(��) {StorySO.name}�� ��尡 �ƴմϴ�.";

		public override void Go(Storyteller storyteller)
		{
			if (Destination == null)
				for (int i = 0; i < NextNodes.Length; i++)
					NextNodes[i]?.Go(storyteller);
			else
				Destination?.Go(storyteller);
		}
	}
}

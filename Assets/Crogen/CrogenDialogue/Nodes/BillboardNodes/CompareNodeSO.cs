using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using Crogen.CrogenDialogue.Billboard;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.BillboardNodes
{
	[CrogenRegisterScript, NodePath("Billboard")]
	public class CompareNodeSO : NodeSO
	{
		[field: SerializeField] public BillboardValueSO TargetValueSO;
		[field: SerializeField] public BillBoardValues Value { get; set; } 

		public override int GetOutputPortCount() => 2;

		public override string[] GetOutputPortsNames() => new[] { "True", "False" };
		public override string GetNodeName() => "Compare";
		public override string GetTooltipText() => "������ �����Կ� ���� True, False���� �����մϴ�.";

		public override void Go(Storyteller storyteller)
		{
			if (CheckTrue())
				NextNodes[0]?.Go(storyteller);
			else
				NextNodes[1]?.Go(storyteller);
		}

		public bool CheckTrue() 
			=> Value.Equal(TargetValueSO.ValueType, TargetValueSO.BillBoardValues);
	}
}

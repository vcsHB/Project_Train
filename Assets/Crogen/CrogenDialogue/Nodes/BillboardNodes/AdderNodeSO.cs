using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using Crogen.CrogenDialogue.Billboard;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.BillboardNodes
{
	[CrogenRegisterScript, NodePath("Billboard")]
	public class AdderNodeSO : NodeSO
    {
		[field: SerializeField] public BillboardValueSO TargetValueSO { get; private set; }
		[field: SerializeField] public BillBoardValues NewValue { get; private set; }

		public override string GetNodeName() => "Adder";
		public override string GetTooltipText()
		{
			if (TargetValueSO)
			{
				switch (TargetValueSO.ValueType)
				{
					case EBillboardValueType.Int:
						return $"{TargetValueSO.BillBoardValues.IntValue}�� {NewValue.IntValue}�� ���մϴ�.";
					case EBillboardValueType.Float:
						return $"{TargetValueSO.BillBoardValues.FloatValue}�� {NewValue.FloatValue}�� ���մϴ�.";
					case EBillboardValueType.Bool:
						return $"{TargetValueSO.BillBoardValues.BoolValue}�� �� ���� ������ �մϴ�.";
					case EBillboardValueType.String:
						return $"{TargetValueSO.BillBoardValues.StringValue}�� {NewValue.StringValue}�� ���մϴ�.";
				}
			}

			return string.Empty;
		}

		public override void Go(Storyteller storyteller)
		{
			switch (TargetValueSO.ValueType)
			{
				case EBillboardValueType.Int:
					TargetValueSO.BillBoardValues.IntValue += NewValue.IntValue;
					break;
				case EBillboardValueType.Float:
					TargetValueSO.BillBoardValues.FloatValue += NewValue.FloatValue;
					break;
				case EBillboardValueType.Bool:
					TargetValueSO.BillBoardValues.BoolValue = !TargetValueSO.BillBoardValues.BoolValue;
					break;
				case EBillboardValueType.String:
					TargetValueSO.BillBoardValues.StringValue += NewValue.StringValue;
					break;
				case EBillboardValueType.Vector2:
					TargetValueSO.BillBoardValues.Vector2Value += NewValue.Vector2Value;
					break;
				case EBillboardValueType.Vector3:
					TargetValueSO.BillBoardValues.Vector3Value += NewValue.Vector3Value;
					break;
			}

			base.Go(storyteller);
		}
	}
}

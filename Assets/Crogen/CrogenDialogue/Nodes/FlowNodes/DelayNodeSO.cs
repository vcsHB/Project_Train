using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using System.Collections;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.FlowNodes
{
	[CrogenRegisterScript, NodePath("Flow")]
	public class DelayNodeSO : NodeSO
	{
		[field: SerializeField] public float Delay { get; private set; } = 0.1f;

		public override string GetNodeName() => "Delay";

		public override string GetTooltipText() => $"{Delay}�� ���� ��ٸ����� ���� ��带 �����մϴ�.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.StartCoroutine(DelayNextGo(storyteller, Delay));
		}

		private IEnumerator DelayNextGo(Storyteller storyteller, float delay)
		{
			yield return new WaitForSeconds(delay);
			base.Go(storyteller);
		}
	}
}

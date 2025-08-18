using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[CrogenRegisterScript] // �⺻ ����� ��. ����ڴ� �̰� ���� �ʿ����
	public class DebugNodeSO : NodeSO
	{
		[TextArea, Delayed] public string message;

		public override string GetNodeName() => "Debug";
		public override string GetTooltipText() => "������ ����Դϴ�.";

		public override void Go(Storyteller storyteller)
		{
			Debug.Log(message);
			base.Go(storyteller);
		}
	}
}
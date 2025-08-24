using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace  Project_Train.Core
{
	public static class InputReader
	{
		public static Controls Controls { get; private set; }
		public static Controls.PlayerActions PlayerActions { get; private set; }
		public static Controls.UIActions UIActions { get; private set; }

		#region Actions

		// �̺�Ʈ�� ���⿡ �ۼ�
		public static event Action<Vector2> OnMoveEvent;

		#endregion

		#region Values

		// ������Ƽ�� ���⿡ �ۼ�

		#endregion

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (Controls != null) return;

			Controls = new Controls();
			PlayerActions = Controls.Player;
			UIActions = Controls.UI;

			PlayerActions.SetCallbacks(new PlayerCallback());
			UIActions.SetCallbacks(new UICallback());

			Controls.Enable();
		}

		#region Callbacks

		private class PlayerCallback : Controls.IPlayerActions
		{
			// �������̽� ������ ���⿡
			public void OnMove(InputAction.CallbackContext context)
			{
				if (context.performed)
					OnMoveEvent.Invoke(context.ReadValue<Vector2>());
			}
		}

		private class UICallback : Controls.IUIActions
		{
			public void OnCancel(InputAction.CallbackContext context)
			{
			}

			public void OnClick(InputAction.CallbackContext context)
			{
			}

			public void OnMiddleClick(InputAction.CallbackContext context)
			{
			}

			public void OnNavigate(InputAction.CallbackContext context)
			{
			}

			public void OnPoint(InputAction.CallbackContext context)
			{
			}

			public void OnRightClick(InputAction.CallbackContext context)
			{
			}

			public void OnScrollWheel(InputAction.CallbackContext context)
			{
			}

			public void OnSubmit(InputAction.CallbackContext context)
			{
			}

			public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
			{
			}

			public void OnTrackedDevicePosition(InputAction.CallbackContext context)
			{
			}
		}

		#endregion
	}
}

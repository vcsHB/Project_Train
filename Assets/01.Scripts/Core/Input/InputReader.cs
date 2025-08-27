using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Project_Train.Core.Input
{
	public static class InputReader
	{
		public static Controls Controls { get; private set; }
		public static Controls.PlayerActions PlayerActions { get; private set; }
		public static Controls.UIActions UIActions { get; private set; }

		#region events

		public static InputEvents Events { get; private set; }
		public static void AddListener(string key, Action action) => Events.AddListener(key, action);
		public static void AddListener<T>(string key, Action<T> action) => Events.AddListener(key, action);

		public static void RemoveListener(string key, Action action) => Events.RemoveListener(key, action);
		public static void RemoveListener<T>(string key, Action<T> action) => Events.RemoveListener(key, action);

		public static void Invoke(string key) => Events.Invoke(key);
		public static void Invoke<T>(string key, T value) => Events.Invoke(key, value);

		#endregion

		#region Values

		// ������Ƽ�� ���⿡ �ۼ�

		#endregion

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (Controls != null) return;

			Events = new InputEvents();
			Controls = new Controls();
			PlayerActions = Controls.Player;
			UIActions = Controls.UI;

			PlayerActions.SetCallbacks(new PlayerCallback());
			UIActions.SetCallbacks(new UICallback());

			SceneManager.sceneUnloaded += HandleClearEvents;

			Controls.Enable();
		}

		private static async void HandleClearEvents(Scene arg0)
		{
			Controls.Disable();
			Events.Clear();
			await Task.Yield();
			Controls.Enable();
		}

		#region Callbacks

		private class PlayerCallback : Controls.IPlayerActions
		{
			public void OnMouse(InputAction.CallbackContext context)
			{
				if (context.performed)
					Events.Invoke("OnMousePositionEvent", context.ReadValue<Vector2>());
			}

			// �������̽� ������ ���⿡
			public void OnMove(InputAction.CallbackContext context)
			{
				if (context.performed)
					Events.Invoke("OnMoveEvent", context.ReadValue<Vector2>());
			}

			public void OnSelect(InputAction.CallbackContext context)
			{
				if (context.performed)
					Events.Invoke("OnSelectEvent");
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

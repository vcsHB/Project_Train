using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace  Project_Train.Core.Input
{
	public static class InputReader
	{
		public static Controls Controls { get; private set; }
		public static Controls.PlayerActions PlayerActions { get; private set; }
		public static Controls.UIActions UIActions { get; private set; }

		#region events

		public static InputEvents Events { get; private set; }
		public static void AddListener(string key, Action action)			=> Events.AddListener(key, action);
		public static void AddListener(string key, Action<float> action)	=> Events.AddListener(key, action);
		public static void AddListener(string key, Action<bool> action)		=> Events.AddListener(key, action);
		public static void AddListener(string key, Action<Vector2> action)	=> Events.AddListener(key, action);
		public static void AddListener(string key, Action<Vector3> action)	=> Events.AddListener(key, action);

		public static void RemoveListener(string key, Action action)			=> Events.RemoveListener(key, action);
		public static void RemoveListener(string key, Action<float> action)		=> Events.RemoveListener(key, action);
		public static void RemoveListener(string key, Action<bool> action)		=> Events.RemoveListener(key, action);
		public static void RemoveListener(string key, Action<Vector2> action)	=> Events.RemoveListener(key, action);
		public static void RemoveListener(string key, Action<Vector3> action)	=> Events.RemoveListener(key, action);

		public static void Invoke(string key)					=> Events.Invoke(key);
		public static void Invoke(string key, float value)		=> Events.Invoke(key, value);
		public static void Invoke(string key, bool value)		=> Events.Invoke(key, value);
		public static void Invoke(string key, Vector2 value)	=> Events.Invoke(key, value);
		public static void Invoke(string key, Vector3 value)	=> Events.Invoke(key, value);

		#endregion

		#region Values

		// 프로퍼티는 여기에 작성

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
			// 인터페이스 구현은 여기에
			public void OnMove(InputAction.CallbackContext context)
			{
				if (context.performed)
					Events.Invoke("OnMoveEvent", context.ReadValue<Vector2>());
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

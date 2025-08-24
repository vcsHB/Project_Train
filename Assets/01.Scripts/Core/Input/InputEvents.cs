using System;
using System.Collections.Generic;

namespace Project_Train.Core.Input
{
	public class InputEvents
	{
		private readonly Dictionary<string, Delegate> _events = new();

		public void Clear()
		{
			_events?.Clear();
		}

		// 리스너 추가
		public void AddListener<T>(string key, Action<T> action)
		{
			if (_events.TryGetValue(key, out var del))
			{
				_events[key] = (Action<T>)del + action;
			}
			else
			{
				_events[key] = action;
			}
		}

		// 파라미터 없는 리스너
		public void AddListener(string key, Action action)
		{
			if (_events.TryGetValue(key, out var del))
			{
				_events[key] = (Action)del + action;
			}
			else
			{
				_events[key] = action;
			}
		}


		// 리스너 제거
		public void RemoveListener<T>(string key, Action<T> action)
		{
			if (_events.TryGetValue(key, out var del))
			{
				var newDel = (Action<T>)del - action;
				if (newDel == null)
					_events.Remove(key);
				else
					_events[key] = newDel;
			}
		}

		public void RemoveListener(string key, Action action)
		{
			if (_events.TryGetValue(key, out var del))
			{
				var newDel = (Action)del - action;
				if (newDel == null)
					_events.Remove(key);
				else
					_events[key] = newDel;
			}
		}

		// 이벤트 호출
		public void Invoke<T>(string key, T arg)
		{
			if (_events.TryGetValue(key, out var del))
			{
				(del as Action<T>)?.Invoke(arg);
			}
		}

		public void Invoke(string key)
		{
			if (_events.TryGetValue(key, out var del))
			{
				(del as Action)?.Invoke();
			}
		}
	}
}

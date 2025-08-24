using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Train.Core.Input
{
	public class InputEvents
	{
		private Dictionary<string, Action> _voidEvents				= new();
		private Dictionary<string, Action<float>> _floatEvents		= new();
		private Dictionary<string, Action<bool>> _boolEvents		= new();
		private Dictionary<string, Action<Vector2>> _vector2Events	= new();
		private Dictionary<string, Action<Vector3>> _vector3Events	= new();

		public void Clear()
		{
			_voidEvents?.Clear();
			_floatEvents?.Clear();
			_boolEvents?.Clear();
			_vector2Events?.Clear();
			_vector3Events?.Clear();
		}

		public void AddListener(string key, Action action)
		{
			if (_voidEvents.ContainsKey(key) == false)
				_voidEvents.Add(key, null);

			_voidEvents[key] += action;
		}
		public void AddListener(string key, Action<float> action)
		{
			if (_floatEvents.ContainsKey(key) == false)
				_floatEvents.Add(key, null);

			_floatEvents[key] += action;
		}
		public void AddListener(string key, Action<bool> action)
		{
			if (_boolEvents.ContainsKey(key) == false)
				_boolEvents.Add(key, null);

			_boolEvents[key] += action;
		}
		public void AddListener(string key, Action<Vector2> action)
		{
			if (_vector2Events.ContainsKey(key) == false)
				_vector2Events.Add(key, null);

			_vector2Events[key] += action;
		}
		public void AddListener(string key, Action<Vector3> action)
		{
			if (_vector3Events.ContainsKey(key) == false)
				_vector3Events.Add(key, null);

			_vector3Events[key] += action;
		}

		public void RemoveListener(string key, Action action) => _voidEvents[key] -= action;
		public void RemoveListener(string key, Action<float> action) => _floatEvents[key] -= action;
		public void RemoveListener(string key, Action<bool> action) => _boolEvents[key] -= action;
		public void RemoveListener(string key, Action<Vector2> action) => _vector2Events[key] -= action;
		public void RemoveListener(string key, Action<Vector3> action) => _vector3Events[key] -= action;

		public void Invoke(string key)
		{
			if (_voidEvents.ContainsKey(key) == false) return;
			_voidEvents[key]?.Invoke();
		}
		public void Invoke(string key, float value)
		{
			if (_floatEvents.ContainsKey(key) == false) return;
			_floatEvents[key]?.Invoke(value);
		}
		public void Invoke(string key, bool value)
		{
			if (_boolEvents.ContainsKey(key) == false) return;
			_boolEvents[key]?.Invoke(value);
		}
		public void Invoke(string key, Vector2 value)
		{
			if (_vector2Events.ContainsKey(key) == false) return;
			_vector2Events[key]?.Invoke(value);
		}
		public void Invoke(string key, Vector3 value)
		{
			if (_vector3Events.ContainsKey(key) == false) return;
			_vector3Events[key]?.Invoke(value);
		}
	}
}

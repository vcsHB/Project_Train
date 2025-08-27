using Project_Train.Core.Input;
using System;
using UnityEngine;

namespace  Project_Train
{
    public class TestInput : MonoBehaviour
    {
        void Start()
        {
            InputReader.AddListener("OnMoveEvent", (Action<Vector2>)HandleMove);
        }

        private void HandleMove(Vector2 vector)
		{
            Debug.Log(vector);
		}

		void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
				InputReader.AddListener("OnMoveEvent", (Action<Vector2>)HandleMove);
			if (Input.GetKeyDown(KeyCode.Alpha2))
				InputReader.RemoveListener("OnMoveEvent", (Action<Vector2>)HandleMove);

		}
	}
}

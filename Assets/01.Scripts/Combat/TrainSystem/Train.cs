using System;
using System.Collections.Generic;

namespace Project_Train.Combat.TrainSystem
{
	public class Train
	{
		public List<CarBase> carList = new();
		public int AllCarCount => carList.Count;

		public float FinalSpeed { get; set; }
		public float CurrentSpeed => FinalSpeed <= 0 ? 0.1f : FinalSpeed;
	}
}

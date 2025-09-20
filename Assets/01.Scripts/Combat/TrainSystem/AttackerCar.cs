using Project_Train.BuildSystem;

namespace  Project_Train.Combat.TrainSystem
{
	public class AttackerCar : FreightCar
	{
		private Building[] _buildings;

		private void Awake()
		{
			_buildings = GetComponentsInChildren<Building>();
		}

		protected override void Update()
		{
			base.Update();

			// TODO : Building;s attack delay setting...
			if (IsRunning)
			{

			}
			else
			{

			}
		}
	}
}

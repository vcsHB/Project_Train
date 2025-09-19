namespace  Project_Train.Combat.TrainSystem
{
    public class PowerCar : CarBase
    {
		public override float TargetSpeed { get; protected set; } = 5f;

		public override void SetHeadCar(CarBase headCar)
		{
			base.SetHeadCar(headCar);

			headCar.SpeedStack += TargetSpeed;
		}
	}
}

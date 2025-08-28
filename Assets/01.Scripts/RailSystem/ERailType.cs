namespace Project_Train.RailSystem
{
	public enum ERailType
	{
		None,

		// 직선
		Straight_NS, // 남-북
		Straight_EW, // 동-서


		// 커브
		Curve_NE,
		Curve_ES,
		Curve_SW,
		Curve_WN,

		// 오르막
		Ascending_N,
		Ascending_E,
		Ascending_S,
		Ascending_W
	}
}
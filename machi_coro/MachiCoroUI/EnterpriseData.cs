namespace MachiCoroUI
{
    public class EnterpriseData
    {
        public string Name { get; set; } = "";
        public int Cost { get; set; }
        public int[] CubeResult { get; set; } = Array.Empty<int>();
        public string Color { get; set; } = "";
        public string? IncomeType { get; set; }
        public int? Income { get; set; }
    }


}
namespace Application.DTO
{
    public class ReqFilterMeteorites : Pagination
    {
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public string? MeteoriteName { get; set; } = String.Empty;
        public string ClassName { get; set; }
        public string SortField { get; set; }
        public bool IsDesc { get; set; }
    }
}

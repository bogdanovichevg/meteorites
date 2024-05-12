namespace Application.DTO
{
    public class MeteoritesFiltersReq : Pagination
    {
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public string? MeteoriteName { get; set; } = String.Empty;
        public string MeteoriteClass { get; set; }
        public string SortableField { get; set; }
        public bool IsDesc { get; set; }
    }
}

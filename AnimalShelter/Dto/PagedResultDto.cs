namespace AnimalShelter.Dto
{
    public record PagedResultDto<T>(IEnumerable<T> Items, int TotalPages, int Page, int PageSize);
}

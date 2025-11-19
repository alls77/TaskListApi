namespace TaskLists.Application.Common;

public class Sort
{
    public string SortBy {  get; set; }
    public bool SortDesc { get; set; }

    public Sort(string sortBy, bool sortDesc)
    {
        SortBy = sortBy;
        SortDesc = sortDesc;
    }
}

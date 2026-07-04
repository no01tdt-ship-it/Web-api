namespace Web_api.ViewModels.AdminMenu
{
    public class DetailAdminMenuViewModel
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public string? ParentName { get; set; }

        public string MenuCode { get; set; } = null!;

        public string MenuName { get; set; } = null!;

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public int SortOrder { get; set; }

        public bool Active { get; set; }

        public bool IsSystem { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<ListAdminMenuViewModel> Children { get; set; } = new List<ListAdminMenuViewModel>();
    }
}

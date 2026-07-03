namespace Web_api.Models
{
    public class AdminMenu
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public string MenuCode { get; set; } = null!;

        public string MenuName { get; set; } = null!;

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public int SortOrder { get; set; }

        public bool Active { get; set; } = true;

        public bool IsSystem { get; set; } = false;

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        // Navigation
        public AdminMenu? Parent { get; set; }

        public ICollection<AdminMenu> Children { get; set; } = new List<AdminMenu>();
    }
}

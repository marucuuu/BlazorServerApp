using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models
{
    public class AssetsQR
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public string AssetType { get; set; } = string.Empty;

        [Required]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public string AssetCondition { get; set; } = string.Empty;
    }
}

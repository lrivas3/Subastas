using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subastas.Domain;

[Table("LogEntries", Schema = "subastas")]
public class LogEntry
{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateLog { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }

        public string? Exception { get; set; }
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public string? State { get; set; }
        public string? InnerExceptionMessage { get; set; }
}
        
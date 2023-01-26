using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pwc.Domain
{
    [Table("PWC_Client")]
    public class Client : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}

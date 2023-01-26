using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwc.Domain.DTOs
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}

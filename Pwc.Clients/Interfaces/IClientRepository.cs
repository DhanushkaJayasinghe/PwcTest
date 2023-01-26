using Pwc.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pwc.Clients.Interfaces
{
    public interface IClientRepository
    {
        Task<List<ClientDto>> GetClients();
        Task<ClientDto> GetClient(Guid clientId);
        Task<bool> Save(ClientDto mClient);
        Task<bool> Delete(Guid clientId);
        Guid ResultGuid { get; set; }
        int Result { get; set; }
        string Message { get; set; }
    }
}

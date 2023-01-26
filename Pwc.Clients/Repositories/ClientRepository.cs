using Microsoft.EntityFrameworkCore;
using Pwc.Clients.Interfaces;
using Pwc.Data;
using Pwc.Domain;
using Pwc.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pwc.Clients.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly Context _context;

        public Guid ResultGuid { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }

        public ClientRepository(Context context) {
            _context = context;
        }

        public async Task<List<ClientDto>> GetClients() {
            try {
                var db = _context;
                var clients = await (from clientQ in db.Clients.Where(m => !m.Deleted).AsNoTracking()
                                     select new ClientDto {
                                         ClientId = clientQ.ClientId,
                                         ClientName = clientQ.ClientName,
                                         Email = clientQ.Email,
                                         JoinedDate = clientQ.JoinedDate
                                     }).Distinct().ToListAsync();
                return clients;
            }
            catch (Exception) {
                //Log error - Error retrieving clients
                throw;
            }
        }

        public async Task<ClientDto> GetClient(Guid clientId) {
            try {
                var db = _context;
                var client = await (from clientQ in db.Clients.Where(m => !m.Deleted && m.ClientId == clientId).AsNoTracking()
                                    select new ClientDto {
                                        ClientId = clientQ.ClientId,
                                        ClientName = clientQ.ClientName,
                                        Email = clientQ.Email,
                                        JoinedDate = clientQ.JoinedDate
                                    }).FirstOrDefaultAsync();
                return client;
            }
            catch (Exception) {
                //Log error - Error retrieving client
                throw;
            }
        }

        public async Task<bool> Save(ClientDto mClient) {
            bool success = false;
            var userId = Guid.Empty;//Logged user id should be taken

            if (!await IsExist(mClient)) {
                var db = _context;
                try {
                    var client = await db.Clients.FirstOrDefaultAsync(m => !m.Deleted && m.ClientId == mClient.ClientId);
                    if (client != null) {
                        client.ClientName = mClient.ClientName;
                        client.Email = mClient.Email;
                        client.JoinedDate = mClient.JoinedDate;
                        client.UpdatedBy = userId;
                        client.UpdatedOn = DateTime.Now;

                        await db.SaveChangesAsync();

                        ResultGuid = client.ClientId;
                        Message = "Record has been updated";
                        success = true;
                    } else {
                        var newClient = new Client {
                            ClientName = mClient.ClientName,
                            Email = mClient.Email,
                            JoinedDate = mClient.JoinedDate,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = Guid.Empty,
                            UpdatedOn = Convert.ToDateTime("01/01/1901"),
                            DeletedBy = Guid.Empty,
                            DeletedOn = Convert.ToDateTime("01/01/1901"),
                            Deleted = false
                        };
                        await db.Clients.AddAsync(newClient);
                        await db.SaveChangesAsync();

                        ResultGuid = newClient.ClientId;
                        Message = "Record has been saved";
                        success = true;
                    }
                }
                catch {
                    Message = "Error occurred while saving";
                    Result = 0;
                    success = false;
                }
            }
            return success;
        }

        public async Task<bool> Delete(Guid clientId) {
            bool success = false;
            var userId = Guid.Empty;//Logged user id should be taken

            var db = _context;
            try {
                var client = await db.Clients.FirstOrDefaultAsync(m => !m.Deleted && m.ClientId == clientId);
                if (client != null) {
                    client.Deleted = true;
                    client.DeletedBy = userId;
                    client.DeletedOn = DateTime.Now;

                    await db.SaveChangesAsync();
                    success = true;
                }
            }
            catch {
                Message = "An error occurred while deleting";
                Result = 0;
                success = false;
            }
            return success;
        }

        private async Task<bool> IsExist(ClientDto mClient) {
            bool exist = false;

            try {
                var db = _context;
                var clientsCount = await db.Clients.CountAsync(m => !m.Deleted && m.ClientId != mClient.ClientId && m.ClientName.Equals(mClient.ClientName.Trim()));
                if (clientsCount > 0) {
                    Message = "Client already exists";
                    exist = true;
                }
            }
            catch (Exception) {
                //Log error - Error retrieving client
                throw;
            }
            return exist;
        }
    }
}

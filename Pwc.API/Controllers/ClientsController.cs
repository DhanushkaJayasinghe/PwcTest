using Microsoft.AspNetCore.Mvc;
using Pwc.Clients.Interfaces;
using Pwc.Domain;
using Pwc.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pwc.API.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository) {
            _clientRepository = clientRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ClientDto>>> Get() {
            try {
                var clients = await _clientRepository.GetClients();
                if (clients.Any()) {
                    return Ok(clients);
                }

                return NoContent();
            }
            catch (Exception) {
                //Log error - Error retrieving clients
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<ClientDto>> Get(Guid clientId) {
            try {
                var client = await _clientRepository.GetClient(clientId);
                if (client != null) {
                    return Ok(client);
                }

                return NoContent();
            }
            catch (Exception) {
                //Log error - Error retrieving client
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Save(ClientDto mClient) {
            try {
                var saved = await _clientRepository.Save(mClient);
                var response = new ResponseStatus {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = saved,
                    Message = _clientRepository.Message
                };
                return Ok(response);
            }
            catch (Exception) {
                //Log error - Error saving client
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update(ClientDto mClient) {
            try {
                var saved = await _clientRepository.Save(mClient);
                var response = new ResponseStatus {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = saved,
                    Message = _clientRepository.Message
                };
                return Ok(response);
            }
            catch (Exception) {
                //Log error - Error saving client
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(Guid clientId) {
            try {
                var deleted = await _clientRepository.Delete(clientId);
                var response = new ResponseStatus {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = deleted,
                    Message = _clientRepository.Message
                };
                return Ok(response);
            }
            catch (Exception) {
                //Log error - Error deleting client
                return BadRequest();
            }
        }
    }
}

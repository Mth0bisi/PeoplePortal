using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeoplePrtal.Models;
using PeoplePrtal.Models.ViewModel;
using PeoplePrtal.Services;
using System;

namespace PeoplePrtal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService _personsService;
        private readonly IAccountsService _accountsService;
        public PersonsController(IPersonsService personsService, IAccountsService accountsService)
        {
            _personsService = personsService;
            _accountsService = accountsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPersons()
        {
            return Ok(await _personsService.GetPersons());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonByIdNumber(string id)
        {
            try
            { 
                var person = await _personsService.FindByIdNumber(id);
                return Ok(person);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Person>>> CreatePerson([FromBody] Person person)
        {
            try
            {
                var dbPerson = await _personsService.FindByIdNumber(person.IdNumber);
                if (dbPerson != null)
                    return BadRequest("Person with same Id already exists.");

                await _personsService.CreatePerson(person);

                return Ok(await _personsService.GetPersons());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person person)
        {
            try
            {
                var dbPerson = await _personsService.FindByIdNumber(person.IdNumber);
                if (dbPerson == null)
                    return BadRequest("Person does not exist.");

                dbPerson.Name = person.Name;
                dbPerson.Surname = person.Surname;
                dbPerson.IdNumber = person.IdNumber;

                await _personsService.UpdatePersonDetails(dbPerson);

                return Ok(await _personsService.GetPersons());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<List<Person>>> DeletePerson(Person person)
        {
            try
            {
                var dbPerson = await _personsService.FindByIdNumber(person.IdNumber);
                if (dbPerson == null)
                    return BadRequest("Person does not exist.");

                var personAccounts = await _accountsService.GetPersonAccounts(person.Code);
                if (personAccounts != null)
                {
                    if (_accountsService.CheckPersonAccountsClosedStatus(personAccounts) == false)
                        return BadRequest("Error deleting person! Please close all accounts and try again.");
                }

                _personsService.DeletePerson(person);

                return Ok(await _personsService.GetPersons());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

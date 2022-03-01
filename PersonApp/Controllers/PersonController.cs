using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonApp.Models;
using PersonApp.Repository;

namespace PersonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personrepo;

        public PersonController(IPersonRepository personrepo)
        {
            _personrepo = personrepo;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetallPersons()
        {
            try
            {
                var persons = await _personrepo.GetallPersonsAsync();
                if (persons.Count() == 0) return NotFound();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please try again later");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById([FromRoute]Guid id)
        {
            try
            {
                var person = await _personrepo.GetPersonByIdAsync(id);
                if (person == null) return NotFound("Person Does Not Exist");
                return Ok(person);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please try again later");
            }
        }
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPersonAsync([FromBody]PersonModel pModel)
        {
            if (!ModelState.IsValid || pModel == null)
            {
                return BadRequest(ModelState);
            }
            var id = await _personrepo.AddPersonAsync(pModel);
            return CreatedAtAction(nameof(GetPersonById) , new {id = id , controller = "Person"} , pModel);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePersonAsync([FromBody] PersonModel pModel , [FromRoute]Guid id)
        {
            if (!ModelState.IsValid || pModel == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var p = await _personrepo.UpdatePersonAsync(pModel , id);
                if(p == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please try again later");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePersonAsync([FromRoute] Guid id)
        {
            try
            {
                var person = await _personrepo.GetPersonByIdAsync(id);
                if (person == null)
                {
                    return NotFound("Submitted Id not found");
                }
                var p1 = await _personrepo.DeletePersonAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please try again later");
            }
        }

    }

}

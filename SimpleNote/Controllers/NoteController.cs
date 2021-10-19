using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleNote.Entities;
using SimpleNote.Entities.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        //private readonly ILogger<NoteController> _logger;
        private readonly IPersistNote _noteRepo;

        public NoteController(
            //ILogger<NoteController> logger,
            IPersistNote noteRepo
        )
        {
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _noteRepo = noteRepo ?? throw new ArgumentNullException(nameof(noteRepo));
        }

        // GET: api/<NoteController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Note>>> Get()
        {
            return Ok(await _noteRepo.Get());
        }

        // GET api/<NoteController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetAsync([Required]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _noteRepo.GetById(id));
        }

        // POST api/<NoteController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([Required, FromBody] string value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return CreatedAtAction("Post", await _noteRepo.Insert(value));
        }

        // PUT api/<NoteController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put([Required]int id, [FromBody] string value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _noteRepo.Update(new Note
            {
                Id = id,
                Text = value
            });
            return NoContent();
        }

        // DELETE api/<NoteController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([Required]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _noteRepo.DeleteById(id);
            return NoContent();
        }
    }
}

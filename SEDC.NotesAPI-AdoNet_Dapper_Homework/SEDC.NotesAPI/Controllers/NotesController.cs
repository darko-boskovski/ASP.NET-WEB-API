using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAPI.Domain.Models;
using SEDC.NotesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, _noteService.GetAllNotes());
        }

        [HttpGet("{id}")]
        public ActionResult<Note> Get(int id)
        {
            return StatusCode(StatusCodes.Status200OK, _noteService.GetNoteById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            _noteService.AddNote(note);
            return StatusCode(StatusCodes.Status201Created, "Note created!");
        }

        [HttpPut]
        public IActionResult Put([FromBody] Note note)
        {
            _noteService.UpdateNote(note);
            return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _noteService.DeleteNote(id);
            return StatusCode(StatusCodes.Status204NoContent, "Note deleted!");
        }
    }
}

using SEDC.NotesAPI.DataAccess;
using SEDC.NotesAPI.Domain.Models;
using SEDC.NotesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesAPI.Services.Implementations
{
    public class NoteService : INoteService
    {
        private IRepository<Note> _noteRepository;
        public NoteService(IRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public void AddNote(Note note)
        {
            _noteRepository.Add(note);
        }

        public void DeleteNote(int id)
        {
            _noteRepository.Delete(id);
        }

        public List<Note> GetAllNotes()
        {
            return _noteRepository.GetAll();
        }

        public Note GetNoteById(int id)
        {
            return _noteRepository.GetById(id);
        }

        public void UpdateNote(Note note)
        {
            _noteRepository.Update(note);
        }
    }
}

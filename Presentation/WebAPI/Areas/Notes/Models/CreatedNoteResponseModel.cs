using System;

namespace Todo.WebAPI.Areas.Notes.Models
{
    public class CreatedNoteResponseModel
    {
        public CreatedNoteResponseModel(Guid noteId)
        {
            NoteId = noteId;
        }

        public Guid NoteId { get; }
    }
}
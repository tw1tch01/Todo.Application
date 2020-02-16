using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.TodoNotes.NoteCommands;
using Todo.DomainModels.TodoNotes;
using Todo.WebAPI.Areas.Items.Controllers;
using Todo.WebAPI.Areas.Notes.Models;
using Todo.WebAPI.Common;

namespace Todo.WebAPI.Areas.Notes.Controllers
{
    [Area(AreaNames.Notes)]
    [Route("api/[controller]")]
    public class NotesController : AbstractController
    {
        private readonly INotesCommandService _commandService;

        public NotesController(INotesCommandService commandService)
        {
            _commandService = commandService;
        }

        /// <summary>
        /// Add a note to an item
        /// </summary>
        /// <remarks>
        /// Add a note to the item specified by <paramref name="itemId"/>.
        /// </remarks>
        /// <param name="itemId">Unique identifier for the item.</param>
        /// <param name="noteDto">Details of the note.</param>
        /// <response code="201">Unique identifier for the added note.</response>
        /// <response code="400">Details in the supplied body aren't valid.</response>
        /// <response code="404">An item with the specified <paramref name="itemId"/> does not exist.</response>
        /// <returns></returns>
        [HttpPost("{itemId:guid}")]
        [ProducesResponseType(typeof(CreatedNoteResponseModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddNoteToItem(Guid itemId, [FromBody]CreateNoteDto noteDto)
        {
            var noteId = await _commandService.CreateNote(itemId, noteDto);

            return CreatedAtAction(nameof(ItemsController.GetItem), new { itemId }, new CreatedNoteResponseModel(noteId));
        }

        /// <summary>
        /// Reply on a note
        /// </summary>
        /// <remarks>
        /// Add a reply to a note on an item.
        /// </remarks>
        /// <param name="noteId">Unique identifier for the note.</param>
        /// <param name="replyDto">Details of the reply.</param>
        /// <response code="201">Unique identifier for the added reply.</response>
        /// <response code="400">Details in the supplied body aren't valid.</response>
        /// <response code="404">A note with the specified <paramref name="noteId"/> does not exist.</response>
        /// <returns></returns>
        [HttpPost("{noteId:guid}/reply")]
        [ProducesResponseType(typeof(CreatedNoteResponseModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ReplyOnNote(Guid noteId, [FromBody]CreateNoteDto replyDto)
        {
            var replyId = await _commandService.ReplyOnNote(noteId, replyDto);

            return CreatedAtAction(nameof(ItemsController.GetItem), nameof(ItemsController), new { itemId = (Guid)default }, new CreatedNoteResponseModel(replyId));
        }

        /// <summary>
        /// Update a note
        /// </summary>
        /// <remarks>
        /// Update the note properties that are are supplied in the <paramref name="noteDto"/>
        /// </remarks>
        /// <param name="noteId">Unique identifier for the note.</param>
        /// <param name="noteDto">Properties with values that should be updated.</param>
        /// <returns></returns>
        /// <response code="204">Note has been updated.</response>
        /// <response code="400">Details in the supplied body aren't valid.</response>
        /// <response code="404">A note with the specified <paramref name="noteId"/> does not exist.</response>
        [HttpPatch("{noteId:guid}")]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateNote(Guid noteId, [FromBody]UpdateNoteDto noteDto)
        {
            await _commandService.UpdateNote(noteId, noteDto);

            return NoContent();
        }

        /// <summary>
        /// Delete a note
        /// </summary>
        /// <remarks>
        /// Delete the note specified by <paramref name="noteId"/>.
        /// </remarks>
        /// <param name="noteId">Unique identifier for the note.</param>
        /// <returns></returns>
        /// <response code="204">Item has been deleted.</response>
        /// <response code="404">A note with the specified <paramref name="noteId"/> does not exist.</response>
        [HttpDelete("{noteId:guid}")]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteNote(Guid noteId)
        {
            await _commandService.DeleteNote(noteId);

            return NoContent();
        }
    }
}
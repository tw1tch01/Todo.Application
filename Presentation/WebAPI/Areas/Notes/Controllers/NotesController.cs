using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.TodoNotes;
using Todo.DomainModels.TodoNotes;
using Todo.Services.TodoNotes.Validation;
using Todo.WebAPI.Areas.Items.Controllers;
using Todo.WebAPI.Common;
using Todo.WebAPI.Extensions;
using Todo.WebAPI.Factories;

namespace Todo.WebAPI.Areas.Notes.Controllers
{
    [Area(AreaNames.Notes)]
    [ApiVersion("1.0")]
    public class NotesController : AbstractController
    {
        private readonly NotesCommandService _commandService;

        public NotesController(NotesCommandService commandService)
        {
            _commandService = commandService;
        }

        /// <summary>
        /// Add a note to an item
        /// </summary>
        /// <remarks>
        /// Add a note to the item specified in the <paramref name="noteDto"/>.
        /// </remarks>
        /// <param name="noteDto">Details of the note.</param>
        /// <response code="201">Unique identifier for the added note.</response>
        /// <response code="400">Details in the supplied body aren't valid.</response>
        /// <response code="404">The item specified in the request body does not exist.</response>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddNoteToItem([FromBody]CreateNoteDto noteDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _commandService.CreateNote(noteDto);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
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
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ReplyOnNote(Guid noteId, [FromBody]CreateNoteDto replyDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _commandService.ReplyOnNote(noteId, replyDto);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
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
        /// <response code="200">Note has been updated.</response>
        /// <response code="400">Details in the supplied body aren't valid.</response>
        /// <response code="404">A note with the specified <paramref name="noteId"/> does not exist.</response>
        [HttpPatch("{noteId:guid}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateNote(Guid noteId, [FromBody]UpdateNoteDto noteDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _commandService.UpdateNote(noteId, noteDto);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        /// <summary>
        /// Delete a note
        /// </summary>
        /// <remarks>
        /// Delete the note specified by <paramref name="noteId"/>.
        /// </remarks>
        /// <param name="noteId">Unique identifier for the note.</param>
        /// <returns></returns>
        /// <response code="200">Note has been deleted.</response>
        /// <response code="404">A note with the specified <paramref name="noteId"/> does not exist.</response>
        [HttpDelete("{noteId:guid}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteNote(Guid noteId)
        {
            var result = await _commandService.DeleteNote(noteId);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        #region Private Methods

        private IActionResult HandleInvalidResult(NoteValidationResult result)
        {
            var response = ApiResponseFactory.CreateResponse(HttpContext, result);

            if (result.GetType().IsTypeOf<NoteNotFoundResult>() || result.GetType().IsTypeOf<ItemNotFoundResult>())
            {
                return NotFound(response);
            }

            return BadRequest(response);
        }

        private IActionResult HandleValidResult(NoteValidationResult result)
        {
            var response = ApiResponseFactory.CreateResponse(HttpContext, result);

            if (result.GetType().IsTypeOf<NoteNotFoundResult>())
            {
                var itemId = result.Data["ItemId"];
                return CreatedAtAction(nameof(ItemsController.GetItem), new { itemId }, response);
            }

            return Ok(response);
        }

        #endregion Private Methods
    }
}
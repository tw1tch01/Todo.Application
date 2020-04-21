using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Data.Common;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.TodoItems;
using Todo.Domain.Enums;
using Todo.DomainModels.TodoItems;
using Todo.DomainModels.TodoItems.Enums;
using Todo.Services.TodoItems.Validation;
using Todo.WebAPI.Common;
using Todo.WebAPI.Extensions;

namespace Todo.WebAPI.Areas.Items.Controllers
{
    [Area(AreaNames.Items)]
    [ApiVersion("1.0")]
    public class ItemsController : AbstractController
    {
        private readonly ItemsCommandService _commandService;
        private readonly ItemsQueryService _queryService;

        public ItemsController(
            ItemsCommandService commandService,
            ItemsQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        /// <summary>
        /// Paged lookup for items
        /// </summary>
        /// <remarks>
        /// Paged lookup for Todo items, filtered by the optional paramaters.
        /// </remarks>
        /// <param name="created_after">Filter items created after this value</param>
        /// <param name="created_before">Filter items created before this value</param>
        /// <param name="search_by">Filter items whose name matches this value</param>
        /// <param name="item_ids">Filter itmes whose itemId is contained within this value</param>
        /// <param name="filter_by_status">Filters items to those whose status matches this value</param>
        /// <param name="filter_by_importance">Filters items to those whose importance matches this value</param>
        /// <param name="filter_by_priority">Filters items to those whose priority matches this value</param>
        /// <param name="sort_by">Sort the items by this value</param>
        /// <returns>Paged collection of items that match the optional parameters</returns>
        /// <response code="200">Paged collection of items that match the optional parameters.</response>
        [HttpGet("")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedCollection<ParentTodoItemLookup>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PagedLookupItems
        (
            [FromQuery] DateTime? created_after = null,
            [FromQuery] DateTime? created_before = null,
            [FromQuery] string search_by = null,
            [FromQuery] ICollection<Guid> item_ids = null,
            [FromQuery] TodoItemStatus? filter_by_status = null,
            [FromQuery] ImportanceLevel? filter_by_importance = null,
            [FromQuery] PriorityLevel? filter_by_priority = null,
            [FromQuery] SortTodoItemsBy? sort_by = null
        )
        {
            (int page, int pageSize) = GetPagination();

            var parameter = new TodoItemLookupParams
            {
                CreatedAfter = created_after,
                CreatedBefore = created_before,
                SearchBy = search_by,
                ItemIds = item_ids,
                FilterByStatus = filter_by_status,
                FilterByImportance = filter_by_importance,
                FilterByPriority = filter_by_priority,
                SortBy = sort_by
            };
            var lookup = await _queryService.PagedLookupParentItems(page, pageSize, parameter);

            return Ok(lookup);
        }

        /// <summary>
        /// Lookup all items
        /// </summary>
        /// <remarks>
        /// Lookup all Todo items, filtered by the optional paramaters.
        /// </remarks>
        /// <param name="created_after">Filter items created after this value</param>
        /// <param name="created_before">Filter items created before this value</param>
        /// <param name="search_by">Filter items whose name matches this value</param>
        /// <param name="item_ids">Filter itmes whose itemId is contained within this value</param>
        /// <param name="filter_by_status">Filters items to those whose status matches this value</param>
        /// <param name="filter_by_importance">Filters items to those whose importance matches this value</param>
        /// <param name="filter_by_priority">Filters items to those whose priority matches this value</param>
        /// <param name="sort_by">Sort the items by this value</param>
        /// <returns>Paged collection of items that match the optional parameters</returns>
        /// <response code="200">Collection of items that match the optional parameters.</response>
        [HttpGet("all")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ParentTodoItemLookup>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LookupAllItems
        (
            [FromQuery] DateTime? created_after = null,
            [FromQuery] DateTime? created_before = null,
            [FromQuery] string search_by = null,
            [FromQuery] ICollection<Guid> item_ids = null,
            [FromQuery] TodoItemStatus? filter_by_status = null,
            [FromQuery] ImportanceLevel? filter_by_importance = null,
            [FromQuery] PriorityLevel? filter_by_priority = null,
            [FromQuery] SortTodoItemsBy? sort_by = null
        )
        {
            var parameter = new TodoItemLookupParams
            {
                CreatedAfter = created_after,
                CreatedBefore = created_before,
                SearchBy = search_by,
                ItemIds = item_ids,
                FilterByStatus = filter_by_status,
                FilterByImportance = filter_by_importance,
                FilterByPriority = filter_by_priority,
                SortBy = sort_by
            };
            var lookup = await _queryService.LookupParentItems(parameter);

            return Ok(lookup);
        }

        /// <summary>
        /// Get an item
        /// </summary>
        /// <remarks>
        /// Get the Todo item specified by <paramref name="itemId"/>.
        /// </remarks>
        /// <param name="itemId">Unique identifier for the item</param>
        /// <returns></returns>
        /// <response code="200">Details about the item.</response>
        /// <response code="404">An item with the specified <paramref name="itemId"/> does not exist.</response>
        [HttpGet("{itemId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TodoItemDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetItem(Guid itemId)
        {
            var item = await _queryService.GetItem(itemId);

            if (item == null) return HandleInvalidResult(new ItemNotFoundResult(itemId));

            return Ok(item);
        }

        /// <summary>
        /// Add an item
        /// </summary>
        /// <remarks>
        /// Add a new Todo item
        /// </remarks>
        /// <param name="itemDto">Details of the new item.</param>
        /// <returns></returns>
        /// <response code="201">Unique identifier for the added item.</response>
        /// <response code="400">Details in the supplied body aren't valid.</response>
        [HttpPost("")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddItem([FromBody]CreateItemDto itemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _commandService.CreateItem(itemDto);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        /// <summary>
        /// Add a child item
        /// </summary>
        /// <remarks>
        /// Add a new child Todo item to the existing Todo item specified by <paramref name="parentItemId"/>
        /// </remarks>
        /// <param name="parentItemId">Unique identifier of the parent item.</param>
        /// <param name="childItemDto">Details of the new child item.</param>
        /// <returns></returns>
        /// <response code="201">Unique identifier for the added child item item.</response>
        /// <response code="400">Details in the supplied body aren't valid.</response>
        /// <response code="404">An item with the specified <paramref name="parentItemId"/> does not exist.</response>
        [HttpPost("{parentItemId:guid}/addchilditem")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AddChildItem(Guid parentItemId, [FromBody]CreateItemDto childItemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _commandService.AddChildItem(parentItemId, childItemDto);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <remarks>
        /// Update the Todo item's properties that are supplied in the <paramref name="itemDto"/>
        /// </remarks>
        /// <param name="itemId">Unique identifier for the item.</param>
        /// <param name="itemDto">Properties with values that should be updated.</param>
        /// <returns></returns>
        /// <response code="200">Item has been updated.</response>
        /// <response code="400">Details in the supplied body aren't valid.</response>
        /// <response code="404">An item with the specified <paramref name="itemId"/> does not exist.</response>
        [HttpPatch("{itemId:guid}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateItem(Guid itemId, [FromBody]UpdateItemDto itemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _commandService.UpdateItem(itemId, itemDto);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <remarks>
        /// Delete the Todo item specified by <paramref name="itemId"/>.
        /// </remarks>
        /// <param name="itemId">Unique identifier for the item.</param>
        /// <returns></returns>
        /// <response code="200">Item has been deleted.</response>
        /// <response code="404">An item with the specified <paramref name="itemId"/> does not exist.</response>
        [HttpDelete("{itemId:guid}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            var result = await _commandService.DeleteItem(itemId);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        #region Actions

        /// <summary>
        /// Cancel an item
        /// </summary>
        /// Cancel the Todo item specified by <paramref name="itemId"/>.
        /// <param name="itemId"></param>
        /// <returns></returns>
        /// <response code="200">Item has been cancelled.</response>
        /// <response code="400">Item cannot be cancelled. See error message.</response>
        /// <response code="404">An item with the specified <paramref name="itemId"/> does not exist.</response>
        [HttpPost("{itemId:guid}/cancel")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CancelItem(Guid itemId)
        {
            var result = await _commandService.CancelItem(itemId);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        /// <summary>
        /// Complete an item
        /// </summary>
        /// Complete the Todo item specified by <paramref name="itemId"/>.
        /// <param name="itemId"></param>
        /// <returns></returns>
        /// <response code="200">Item has been completed.</response>
        /// <response code="400">Item cannot be completed. See error message.</response>
        /// <response code="404">An item with the specified <paramref name="itemId"/> does not exist.</response>
        [HttpPost("{itemId:guid}/complete")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CompleteItem(Guid itemId)
        {
            var result = await _commandService.CompleteItem(itemId);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        /// <summary>
        /// Reset an item
        /// </summary>
        /// Reset the Todo item specified by <paramref name="itemId"/>.
        /// <param name="itemId"></param>
        /// <returns></returns>
        /// <response code="200">Item has been cancelled.</response>
        /// <response code="404">An item with the specified <paramref name="itemId"/> does not exist.</response>
        [HttpPost("{itemId:guid}/reset")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ResetItem(Guid itemId)
        {
            var result = await _commandService.ResetItem(itemId);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        /// <summary>
        /// Start an item
        /// </summary>
        /// Start the Todo item specified by <paramref name="itemId"/>.
        /// <param name="itemId"></param>
        /// <returns></returns>
        /// <response code="200">Item has been started.</response>
        /// <response code="400">Item cannot be started. See error message.</response>
        /// <response code="404">An item with the specified <paramref name="itemId"/> does not exist.</response>
        [HttpPost("{itemId:guid}/start")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> StartItem(Guid itemId)
        {
            var result = await _commandService.StartItem(itemId);

            if (result.IsValid) return HandleValidResult(result);

            return HandleInvalidResult(result);
        }

        #endregion Actions

        #region Private Methods

        private IActionResult HandleInvalidResult(ItemValidationResult result)
        {
            var response = new ApiResponse(HttpContext, result);

            if (result.GetType().IsTypeOf<ItemNotFoundResult>())
            {
                return NotFound(response);
            }

            return BadRequest(response);
        }

        private IActionResult HandleValidResult(ItemValidationResult result)
        {
            var response = new ApiResponse(HttpContext, result);

            if (result.GetType().IsTypeOf<ItemCreatedResult>())
            {
                var itemId = result.Data["ItemId"];
                return CreatedAtAction(nameof(GetItem), new { itemId }, response);
            }

            return Ok(response);
        }

        #endregion Private Methods
    }
}
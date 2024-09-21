using _123Sales.Application.Commands;
using _123Sales.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

/// <summary>
/// Controller to manage sales operations such as creating, retrieving, updating, and deleting sales.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the SaleController with the provided mediator for command and
    /// query handling.
    /// </summary>
    /// <param name="mediator">The IMediator instance for handling commands and queries.</param>
    public SaleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cancels an item from an existing sale.
    /// </summary>
    /// <param name="saleId">The ID of the sale containing the item to cancel.</param>
    /// <param name="itemId">The ID of the item to cancel.</param>
    /// <returns>
    /// Returns 204 No Content if the item cancellation is successful; 404 Not Found if the sale or
    /// item does not exist.
    /// </returns>
    [HttpDelete("{saleId}/items/{itemId}")]
    public async Task<IActionResult> CancelItem(Guid saleId, Guid itemId)
    {
        try
        {
            Log.Information("Received request to cancel item with Id: {ItemId} from sale with Id: {SaleId}", itemId, saleId);

            var result = await _mediator.Send(new CancelItemCommand { SaleId = saleId, ItemId = itemId });
            if (!result)
            {
                Log.Warning("Sale with Id: {SaleId} or item with Id: {ItemId} not found", saleId, itemId);
                return NotFound();
            }

            Log.Information("Item with Id: {ItemId} successfully cancelled from sale with Id: {SaleId}", itemId, saleId);
            return NoContent();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while cancelling item with Id: {ItemId} from sale with Id: {SaleId}", itemId, saleId);
            return StatusCode(500, "An error occurred while cancelling the item.");
        }
    }

    /// <summary>
    /// Creates a new sale record.
    /// </summary>
    /// <param name="command">The command containing sale details to create a new sale.</param>
    /// <returns>Returns the created sale ID and a 201 Created response if successful.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
    {
        try
        {
            Log.Information("Received request to create sale for CustomerId: {CustomerId} at {Time}", command.CustomerId, DateTime.Now);

            var saleId = await _mediator.Send(command);
            Log.Information("Sale created successfully with Id: {SaleId}", saleId);

            return CreatedAtAction(nameof(GetSaleById), new { id = saleId }, saleId);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while creating sale for CustomerId: {CustomerId}", command.CustomerId);
            return StatusCode(500, "An error occurred while creating the sale.");
        }
    }

    /// <summary>
    /// Deletes a sale by sale ID.
    /// </summary>
    /// <param name="id">The ID of the sale to delete.</param>
    /// <returns>
    /// Returns 204 No Content if deletion is successful; 404 Not Found if the sale does not exist.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        try
        {
            Log.Information("Received request to delete sale with Id: {SaleId}", id);

            var result = await _mediator.Send(new DeleteSaleCommand { SaleId = id });
            if (!result)
            {
                Log.Warning("Sale with Id: {SaleId} not found for deletion", id);
                return NotFound();
            }

            Log.Information("Sale with Id: {SaleId} successfully deleted", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while deleting sale with Id: {SaleId}", id);
            return StatusCode(500, "An error occurred while deleting the sale.");
        }
    }

    /// <summary>
    /// Retrieves the sale details by sale ID.
    /// </summary>
    /// <param name="id">The ID of the sale to retrieve.</param>
    /// <returns>Returns the sale details if found; otherwise, returns a 404 Not Found response.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleById(Guid id)
    {
        try
        {
            Log.Information("Received request to fetch sale details for SaleId: {SaleId} at {Time}", id, DateTime.Now);

            var sale = await _mediator.Send(new GetSaleByIdQuery { SaleId = id });

            if (sale == null)
            {
                Log.Warning("Sale with Id: {SaleId} not found", id);
                return NotFound();
            }

            Log.Information("Successfully fetched sale details for SaleId: {SaleId}", id);
            return Ok(sale);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while fetching sale details for SaleId: {SaleId}", id);
            return StatusCode(500, "An error occurred while fetching the sale details.");
        }
    }

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="id">The ID of the sale to update.</param>
    /// <param name="command">The command containing updated sale details.</param>
    /// <returns>
    /// Returns 204 No Content if the update is successful; 404 Not Found if the sale does not
    /// exist; 400 Bad Request if the sale ID in the path does not match the command.
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleCommand command)
    {
        if (id != command.SaleId)
        {
            Log.Warning("Mismatch between URL sale ID: {SaleId} and body sale ID: {CommandSaleId}", id, command.SaleId);
            return BadRequest("The sale ID in the path does not match the sale ID in the body.");
        }

        try
        {
            Log.Information("Received request to update sale with Id: {SaleId}", id);

            var result = await _mediator.Send(command);
            if (!result)
            {
                Log.Warning("Sale with Id: {SaleId} not found for update", id);
                return NotFound();
            }

            Log.Information("Sale with Id: {SaleId} successfully updated", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while updating sale with Id: {SaleId}", id);
            return StatusCode(500, "An error occurred while updating the sale.");
        }
    }
}
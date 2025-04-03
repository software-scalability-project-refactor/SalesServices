using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities;
using SalesService.Repositories;

namespace SalesService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IRepository<Sale> _repository;

    public SalesController(IRepository<Sale> repository)
    {
        _repository = repository;
    }

    // GET: api/sales
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sale>>> GetAllSales()
    {
        var sales = await _repository.GetAllAsync();
        return Ok(sales);
    }

    // GET: api/sales/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> GetSaleById(Guid id)
    {
        var sale = await _repository.GetByIdAsync(id);
        
        if (sale == null)
        {
            return NotFound();
        }
        
        return Ok(sale);
    }

    // POST: api/sales
    [HttpPost]
    public async Task<ActionResult<Sale>> CreateSale([FromBody] Sale sale)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdSale = await _repository.AddAsync(sale);
            return CreatedAtAction(nameof(GetSaleById), new { id = createdSale.IdSale }, createdSale);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // PUT: api/sales/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] Sale sale)
    {
        if (id != sale.IdSale)
        {
            return BadRequest("ID mismatch");
        }

        if (!await _repository.ExistsAsync(id))
        {
            return NotFound();
        }

        try
        {
            await _repository.UpdateAsync(sale);
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }
            throw;
        }
    }

    // DELETE: api/sales/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var sale = await _repository.GetByIdAsync(id);
        if (sale == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
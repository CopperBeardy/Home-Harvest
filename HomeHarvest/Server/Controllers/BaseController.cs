using AutoMapper;
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeHarvest.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BaseController<T> : ControllerBase where T : BaseEntity 
{
	protected readonly ApplicationDbContext _context;


	public readonly ILogger _logger;

	public BaseController(ApplicationDbContext context, ILogger logger)
	{
		this._logger = logger;		
		this._context = context;

	}

	[HttpGet]
	public virtual async Task<ActionResult<IEnumerable<T>>> GetAll()=>
		Ok( await _context.Set<T>().AsNoTracking().ToListAsync());


	[HttpGet("{id}")]
	public virtual async Task<ActionResult<T>> Get(int id)
	{
	
	var entity = await _context.Set<T>()
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.Id == id);

		if (entity != null)
		{
			return Ok(entity);
		}

		return NotFound();
	}

	[HttpPost]
	public virtual async Task<ActionResult> Post(T entity)
	{
		try
		{
			
			_context.Add(entity);
			await _context.SaveChangesAsync();
			_logger.LogInformation($"New {entity.GetType()} with Id {entity.Id} has been added to the Db ");

			return Ok();
		}
		catch (Exception ex)
		{
			_logger.LogError($"Exception occured trying to insert {entity.GetType()} to database: {ex}");
			return BadRequest();
		}
	}

	[HttpPut]
	public virtual async Task<IActionResult> Put(T entity)
	{
		
		_context.Update(entity);
		try
		{
			await _context.SaveChangesAsync();
			_logger.LogInformation($"{entity.GetType()} with Id {entity.Id} has been modified in the Db ");
			return Ok();
		}
		catch (Exception ex)
		{
			_logger.LogError($" {entity.GetType()} with Id {entity.Id} has throw a exception", ex);
			return BadRequest();
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var entity = await _context.Set<T>().FindAsync(id);
		if (entity == null)
		{
			_logger.LogError($"entity could not be found", id);
			return NotFound(id);
		}

		_context.Set<T>().Remove(entity);
		await _context.SaveChangesAsync();
		_logger.LogInformation($"{entity.GetType()} with Id {entity.Id} has been removed Db ");
		return Ok();
	}


	protected bool EntityExists(int id) => _context.Set<T>().Any(x => x.Id == id);
}

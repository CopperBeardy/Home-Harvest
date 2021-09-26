using AutoMapper;
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Entities;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeHarvest.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BaseController<TEntity, TModel> : ControllerBase where TEntity : BaseEntity where TModel : BaseDto
{
	protected readonly ApplicationDbContext _context;

	public readonly IMapper _mapper;
	public readonly ILogger _logger;

	public BaseController(ApplicationDbContext context, ILogger logger, IMapper mapper)
	{
		this._logger = logger;
		this._mapper = mapper;
		this._context = context;

	}

	[HttpGet]
	public virtual async Task<ActionResult<IEnumerable<TModel>>> GetAll()
	{
		//var entities = await this.repository.GetAll();
		var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
		var mapped = _mapper.Map<List<TModel>>(entities);
		return Ok(mapped);
	}

	[HttpGet("{id}")]
	public virtual async Task<ActionResult<TModel>> Get(int id)
	{

		var entity = await _context.Set<TEntity>()
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.Id == id);

		var mapped = _mapper.Map<TModel>(entity);

		if (mapped != null)
		{
			return Ok(mapped);
		}

		return NotFound();
	}

	[HttpPost]
	public virtual async Task<ActionResult> Post(TModel model)
	{
		try
		{
			var entity = _mapper.Map<TEntity>(model);
			_context.Add(entity);
			await _context.SaveChangesAsync();
			_logger.LogInformation($"New {entity.GetType()} with Id {entity.Id} has been added to the Db ");

			return Ok();
		}
		catch (Exception ex)
		{
			_logger.LogError($"Exception occured trying to insert {model.GetType()} to database: {ex}");
			return BadRequest();
		}
	}

	[HttpPut]
	public virtual async Task<IActionResult> Put(TModel model)
	{
		var entity = _mapper.Map<TEntity>(model);
		_context.Update(entity);
		try
		{
			await _context.SaveChangesAsync();
			_logger.LogInformation($"{model.GetType()} with Id {model.Id} has been modified in the Db ");
			return Ok();
		}
		catch (Exception ex)
		{
			_logger.LogError($" {model.GetType()} with Id {model.Id} has throw a exception", ex);
			return BadRequest();
		}
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var entity = await _context.Set<TEntity>().FindAsync(id);
		if (entity == null)
		{
			_logger.LogError($"entity could not be found", id);
			return NotFound(id);
		}

		_context.Set<TEntity>().Remove(entity);
		await _context.SaveChangesAsync();
		_logger.LogInformation($"{entity.GetType()} with Id {entity.Id} has been removed Db ");
		return Ok();
	}


	protected bool EntityExists(int id) => _context.Set<TEntity>().Any(x => x.Id == id);
}

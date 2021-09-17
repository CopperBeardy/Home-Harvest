using AutoMapper;
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Entities;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHarvest.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BaseController<TEntity, TModel> : ControllerBase where TEntity : BaseEntity where TModel : BaseDto
{
    protected readonly ApplicationDbContext _context;
    protected readonly ILogger<BaseController<TEntity,TModel>> _logger;
    protected readonly IMapper _mapper;
    public BaseController(ApplicationDbContext context, ILogger<BaseController<TEntity,TModel>> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TModel>>> GetAll()
    {
        var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        var mapModels = _mapper.Map<List<TModel>>(entities);
        return Ok(mapModels);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TModel>> Get(int id)
    {
        if (EntityExists(id))
        {
            var entity = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(id));
            var mappedModel = _mapper.Map<TModel>(entity);
            return Ok(mappedModel);
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
            return StatusCode(500);
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
            return StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            _logger.LogError($"entity could not be found", entity.ToString());
            return NotFound();
        }

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"{entity.GetType()} with Id {entity.Id} has been removed Db ");
        return Ok();
    }


    protected bool EntityExists(int id) => _context.Set<TEntity>().Any(e => e.Id == id);
}

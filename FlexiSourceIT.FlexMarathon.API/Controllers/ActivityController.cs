using FlexiSourceIT.FlexMarathon.API.Helpers;
using FlexiSourceIT.FlexMarathon.Application.Interfaces.Services;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlexiSourceIT.FlexMarathon.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController(ILogger<ActivityController> logger, IActivityService activityService) : 
    APIControllerBase<ActivityController>(logger)
{
    private readonly IActivityService _activityService = activityService;

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddActivityAsync([FromBody] ActivityModel activity)
    {
        var addedActivity = await _activityService.AddActivityAsync(activity);
        if (addedActivity == null)
        {
            return BadRequest("Failed to add activity.");
        }
        return CreatedAtAction(nameof(GetActivityAsync), new { activityId = addedActivity.Id }, addedActivity);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateActivityAsync([FromBody] ActivityModel activity)
    {
        await _activityService.UpdateActivityAsync(activity);
        return NoContent();
    }

    [HttpGet]
    [Route("{activityId:guid}")]
    public async Task<IActionResult> GetActivityAsync(Guid activityId)
    {
        var activity = await _activityService.GetActivityAsync(activityId);
        if (activity == null)
        {
            return NotFound();
        }
        return Ok(activity);
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetActivityAsync([FromQuery] string filterExpression)
    {
        try
        {
            var filter = FilterParser.Parse<ActivityModel>(filterExpression);
            var activity = await _activityService.GetActivityAsync(filter);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to parse filter expression.");
            return BadRequest("Invalid filter expression.");
        }
    }

    [HttpGet]
    [Route("filter-by-expression")]
    public async Task<IActionResult> GetActivityByExpressionAsync([FromQuery] string filterExpression)
    {
        try
        {
            var filter = FilterParser.Parse<ActivityModel>(filterExpression);
            var activity = await _activityService.GetActivityAsync(filter);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to parse filter expression.");
            return BadRequest("Invalid filter expression.");
        }
    }

    [HttpGet]
    [Route("filtered")]
    public async Task<IActionResult> GetActivitiesByFilterAsync([FromQuery] string filterExpression)
    {
        try
        {
            var filter = FilterParser.Parse<ActivityModel>(filterExpression);
            var activities = await _activityService.GetActivitiesByFilterAsync(filter);
            if (activities == null || !activities.Any())
            {
                return NotFound();
            }
            return Ok(activities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to parse filter expression.");
            return BadRequest("Invalid filter expression.");
        }
    }

    [HttpGet]
    [Route("user/{userProfileId:guid}")]
    public async Task<IActionResult> GetActivitiesAsync(Guid userProfileId)
    {
        var activities = await _activityService.GetActivitiesAsync(userProfileId);
        if (activities == null || !activities.Any())
        {
            return NotFound();
        }
        return Ok(activities);
    }

    [HttpGet]
    [Route("filter-with-model")]
    public async Task<IActionResult> GetActivityWithModelAsync([FromQuery] ActivityModel filter)
    {
        try
        {
            var activity = await _activityService.GetActivityAsync(filter);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve activity with the given model.");
            return BadRequest("Failed to retrieve activity.");
        }
    }

    [HttpGet]
    [Route("filter-with-models")]
    public async Task<IActionResult> GetActivitiesWithModelAsync([FromQuery] ActivityModel filter)
    {
        try
        {
            var activities = await _activityService.GetActivitiesAsync(filter);
            if (activities == null || !activities.Any())
            {
                return NotFound();
            }
            return Ok(activities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve activities with the given model.");
            return BadRequest("Failed to retrieve activities.");
        }
    }
}

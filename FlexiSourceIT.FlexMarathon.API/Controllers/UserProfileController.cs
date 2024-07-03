using FlexiSourceIT.FlexMarathon.API.Helpers;
using FlexiSourceIT.FlexMarathon.Application.Interfaces.Services;
using FlexiSourceIT.FlexMarathon.Application.Models.Data;
using Microsoft.AspNetCore.Mvc;
using SMEAppHouse.Core.CodeKits.Helpers;

namespace FlexiSourceIT.FlexMarathon.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserProfileController(
    ILogger<UserProfileController> logger,
    IUserProfileService userProfileService
    ) : APIControllerBase<UserProfileController>(logger)
{
    private readonly IUserProfileService _userProfileService = userProfileService;

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddUserProfileAsync([FromBody] UserProfileModel userProfile)
    {
        var addedUserProfile = await _userProfileService.AddUserProfileAsync(userProfile);
        if (addedUserProfile == null)
        {
            return BadRequest("Failed to add user profile.");
        }
        return CreatedAtAction(nameof(GetUserProfileAsync), new { userProfileId = addedUserProfile.Id }, addedUserProfile);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UserProfileModel userProfile)
    {
        await _userProfileService.UpdateUserProfileAsync(userProfile);
        return NoContent();
    }

    [HttpGet]
    [Route("{userProfileId:guid}")]
    public async Task<IActionResult> GetUserProfileAsync(Guid userProfileId)
    {
        var userProfile = await _userProfileService.GetUserProfileAsync(userProfileId);
        if (userProfile == null)
        {
            return NotFound();
        }
        return Ok(userProfile);
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetUserProfileAsync([FromQuery] string modelFilter)
    {
        try
        {
            // Convert the string filter to a lambda expression
            var filterExpression = FilterParser.Parse<UserProfileModel>(modelFilter);

            var userProfile = await _userProfileService.GetUserProfileAsync(filterExpression);
            if (userProfile == null)
                return NotFound();

            return Ok(userProfile);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogDebug(ex.GetExceptionMessages());
            return BadRequest("Invalid filter expression.");
        }
    }

    [HttpGet]
    [Route("filtered")]
    public async Task<IActionResult> GetUserProfilesAsync([FromQuery] string modelFilter)
    {
        try
        {
            // Convert the string filter to a lambda expression
            var filterExpression = FilterParser.Parse<UserProfileModel>(modelFilter);

            var userProfiles = await _userProfileService.GetUserProfilesAsync(filterExpression);
            if (userProfiles == null || !userProfiles.Any())
            {
                return NotFound();
            }
            return Ok(userProfiles);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogDebug(ex.GetExceptionMessages());
            return BadRequest("Invalid filter expression.");
        }
    }
}

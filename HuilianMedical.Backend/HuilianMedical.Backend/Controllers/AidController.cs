using HuilianMedical.Backend.Models;
using HuiLianMedical.Share;
using HuiLianMedical.Share.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HuilianMedical.Backend.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
public class AidController(MedicalContext context, IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    [TokenActionFilter]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> AddAid(AidModel aidModel)
    {
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        member = await context.Users.FirstOrDefaultAsync(x => x.Id == member.Id);
        if (member == null) return NotFound();

        if (member.Identity == "") return Ok();
        
        await context.TodoAid.AddAsync(aidModel);

        return Ok();
    }
}
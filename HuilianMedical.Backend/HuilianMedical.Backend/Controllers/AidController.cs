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

        aidModel.Key = member.Id;
        
        await context.TodoAid.AddAsync(aidModel);

        return Ok();
    }
    
    [TokenActionFilter]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> AddImage([FromBody] IFormFile file)
    {
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        member = await context.Users.FirstOrDefaultAsync(x => x.Id == member.Id);
        if (member == null) return NotFound();
        
        var aid = await context.TodoAid.FirstOrDefaultAsync(x => x.Key == member.Id);
        if (aid == null) return NotFound();

        if (member.Identity == "急救员") return Ok();
        
        if (!Directory.Exists($"/Aid/{member.Id}"))
            Directory.CreateDirectory($"/Aid/{member.Id}");
        
        if (!string.IsNullOrEmpty(aid.Image))
        {
            System.IO.File.Delete(aid.Image);
        }

        var s = System.IO.File.Open($"/Aid/{member.Id}/{file.FileName}", FileMode.OpenOrCreate);
        try
        {
            await file.CopyToAsync(s);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        aid.Image = $"/Aid/{member.Id}/{file.FileName}";

        return Ok();
    }
}
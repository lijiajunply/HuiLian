using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HuiLianMedical.WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace HuiLianMedical.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(MedicalContext context, JwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    [TokenActionFilter]
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserModel>> GetData()
    {
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        
        member = await context.Users.FirstOrDefaultAsync(x => x.Id == member.Id);
        if (member == null) return NotFound();
        return member;
    }

    [HttpPost]
    public async Task<ActionResult<string>> SignUp(UserModel model)
    {
        if (DateTime.Today.Month != 10)
            return NotFound();

        if (context.Users == null!)
        {
            return Problem("Entity set 'MemberContext.Students'  is null.");
        }

        context.Users.Add(model);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (await MemberModelExists(model.Id))
                return Conflict();

            throw;
        }

        //return MemberModel.AutoCopy<SignModel, MemberModel>(model);
        return jwtHelper.GetMemberToken(model);
    }


    [HttpPost]
    public async Task<ActionResult<string>> Login(LoginModel loginModel)
    {
        if (context.Users == null!)
            return NotFound();

        var model =
            await context.Users.FirstOrDefaultAsync(x =>
                (x.Phone == loginModel.Phone || x.Email == loginModel.Email) && x.UserName == loginModel.UserName);

        if (model == null) return NotFound();

        return jwtHelper.GetMemberToken(model);
    }
    
    private async Task<bool> MemberModelExists(string id)
    {
        return await context.Users.AnyAsync(e => e.Id == id);
    }
}
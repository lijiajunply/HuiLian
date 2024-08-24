using HuilianMedical.Backend.Models;
using HuiLianMedical.Share;
using HuiLianMedical.Share.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HuilianMedical.Backend.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController(MedicalContext context, JwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    
    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
    [TokenActionFilter]
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserModel>> GetData()
    {
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null)
        {
            Console.WriteLine("未登录");
            return NotFound();
        }

        member = await context.Users.Include(x => x.Commodities)
            .FirstOrDefaultAsync(x => x.Id == member.Id);
        if (member == null)
        {
            Console.WriteLine("未找到用户");
            return NotFound();
        }
        return member;
    }

    
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="model">用户数据</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<string>> SignUp(UserModel model)
    {
        model.Id = model.ToString();
        
        context.Users.Add(model);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (await context.Users.AnyAsync(e => e.Id == model.Id))
                return Conflict();

            throw;
        }

        //return MemberModel.AutoCopy<SignModel, MemberModel>(model);
        return jwtHelper.GetMemberToken(model);
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="loginModel">登录信息</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<string>> Login(LoginModel loginModel)
    {
        if (context.Users == null!)
            return NotFound();

        var model =
            await context.Users.FirstOrDefaultAsync(x =>
                x.Phone == loginModel.Phone && x.Password == loginModel.Password);

        if (model == null) return NotFound();
        
        Console.WriteLine(model);

        return jwtHelper.GetMemberToken(model);
    }

    
    /// <summary>
    /// 更新头像
    /// </summary>
    /// <param name="file">图片文件</param>
    /// <returns></returns>
    [TokenActionFilter]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UploadAvatar(IFormFile file)
    {
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        member = await context.Users.FirstOrDefaultAsync(x => x.Id == member.Id);
        if (member == null) return NotFound();
        if (!Directory.Exists($"/UserAva/{member.Id}"))
            Directory.CreateDirectory($"/UserAva/{member.Id}");

        if (!string.IsNullOrEmpty(member.Avatar))
        {
            System.IO.File.Delete(member.Avatar);
        }

        var s = System.IO.File.Open($"/UserAva/{member.Id}/{file.FileName}", FileMode.OpenOrCreate);
        try
        {
            await file.CopyToAsync(s);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        member.Avatar = $"/UserAva/{member.Id}/{file.FileName}";
        await context.SaveChangesAsync();
        return Ok();
    }
}
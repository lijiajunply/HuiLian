using HuiLianMedical.Share;
using HuiLianMedical.Share.Data;
using HuiLianMedical.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HuiLianMedical.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CommodityController(MedicalContext context, IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    [HttpGet("{category}")]
    public async Task<ActionResult<List<CommodityModel>>> GetCommodityFromCategory(string category)
    {
        var c = await context.Categories
            .Include(categoryModel => categoryModel.Commodities)
            .FirstOrDefaultAsync(x => x.Key == category);
        if (c == null) return NotFound();
        return c.Commodities;
    }

    [TokenActionFilter]
    [Authorize]
    [HttpGet("{key}")]
    public async Task<ActionResult> Shopping(string key)
    {
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        member = await context.Users.FirstOrDefaultAsync(x => x.Id == member.Id);
        if (member == null) return NotFound();

        var commodity = await context.Commodities.FirstOrDefaultAsync(x => x.Key == key);
        if (commodity == null) return NotFound();
        if (member.Points < commodity.Price) return BadRequest("余额不足");
        member.Points -= commodity.Price;

        member.Commodities.Add(commodity);
        await context.SaveChangesAsync();
        return Ok(member.Points);
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryModel>>> GetAllCategory()
    {
        return await context.Categories.Include(categoryModel => categoryModel.Commodities).ToListAsync();
    }

    [TokenActionFilter]
    [Authorize(Roles = "管理员")]
    [HttpPost]
    public async Task<ActionResult> AddCategory(CategoryModel categoryModel)
    {
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        member = await context.Users.FirstOrDefaultAsync(x => x.Id == member.Id);
        if (member is not { Identity: "管理员" }) return NotFound();

        var category = string.IsNullOrEmpty(categoryModel.Key)
            ? null
            : await context.Categories.FirstOrDefaultAsync(x => x.Key == categoryModel.Key);
        if (category != null)
        {
            category.Name = categoryModel.Name;
            category.Description = categoryModel.Description;
        }
        else
        {
            context.Categories.Add(categoryModel);
        }

        await context.SaveChangesAsync();
        return Ok();
    }

    [TokenActionFilter]
    [Authorize(Roles = "管理员")]
    [HttpPost]
    public async Task<ActionResult> AddCommodity(CommodityModel commodityModel)
    {
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        member = await context.Users.FirstOrDefaultAsync(x => x.Id == member.Id);
        if (member is not { Identity: "管理员" }) return NotFound();

        var commodity = string.IsNullOrEmpty(commodityModel.Key)
            ? null
            : await context.Commodities.FirstOrDefaultAsync(x => x.Key == commodityModel.Key);

        if (commodity != null)
        {
            commodity.Name = commodityModel.Name;
            commodity.Description = commodityModel.Description;
            commodity.Price = commodityModel.Price;
            commodity.Stock = commodityModel.Stock;
        }
        else
        {
            context.Commodities.Add(commodityModel);
        }

        await context.SaveChangesAsync();
        return Ok();
    }

    [TokenActionFilter]
    [Authorize(Roles = "管理员")]
    [HttpPost]
    public async Task<ActionResult> CommodityAddCategory(CommodityAddCategory data)
    {
        var commodity = await context.Commodities.FirstOrDefaultAsync(x => x.Key == data.CommodityKey);
        if (commodity == null) return NotFound();
        var category = await context.Categories
            .Include(categoryModel => categoryModel.Commodities)
            .FirstOrDefaultAsync(x => x.Key == data.CategoryKey);
        if (category == null) return NotFound();
        if (!category.Commodities.Contains(commodity))
            category.Commodities.Add(commodity);
        await context.SaveChangesAsync();
        return Ok();
    }

    [TokenActionFilter]
    [Authorize(Roles = "管理员")]
    [HttpPost]
    public async Task<ActionResult> CommodityRemoveCategory(CommodityAddCategory data)
    {
        var commodity = await context.Commodities.FirstOrDefaultAsync(x => x.Key == data.CommodityKey);
        if (commodity == null) return NotFound();
        var category = await context.Categories
            .Include(categoryModel => categoryModel.Commodities)
            .FirstOrDefaultAsync(x => x.Key == data.CategoryKey);
        if (category == null) return NotFound();
        category.Commodities.Remove(commodity);
        await context.SaveChangesAsync();
        return Ok();
    }

    [TokenActionFilter]
    [Authorize(Roles = "管理员")]
    [HttpGet("{key}")]
    public async Task<ActionResult> CommodityRemove(string key)
    {
        var commodity = await context.Commodities.FirstOrDefaultAsync(x => x.Key == key);
        if (commodity == null) return NotFound();
        context.Commodities.Remove(commodity);
        await context.SaveChangesAsync();

        return Ok();
    }

    [TokenActionFilter]
    [Authorize(Roles = "管理员")]
    [HttpGet("{key}")]
    public async Task<ActionResult> CategoryRemove(string key)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Key == key);
        if (category == null) return NotFound();
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return Ok();
    }

    [TokenActionFilter]
    [Authorize]
    [HttpPut("{key}")]
    public async Task<ActionResult> UploadCommodityImage([FromBody] IFormFile file, string key)
    {
        var commodity = await context.Commodities.FirstOrDefaultAsync(x => x.Key == key);
        if (commodity == null) return NotFound();
        
        if (!Directory.Exists($"/Commodity/{commodity.Key}"))
            Directory.CreateDirectory($"/Commodity/{commodity.Key}");

        var s = System.IO.File.Open($"/Commodity/{commodity.Key}/{file.FileName}", FileMode.OpenOrCreate);
        try
        {
            await file.CopyToAsync(s);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        commodity.Image = $"/Commodity/{commodity.Key}/{file.FileName}";
        await context.SaveChangesAsync();
        return Ok();
    }
}

public record CommodityAddCategory(string CommodityKey, string CategoryKey);
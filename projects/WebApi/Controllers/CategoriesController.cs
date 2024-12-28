using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.CategoryRequestDtos;
using EntityLayer.Dtos.ResponseDtos.CategoryResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CategoriesController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        Response<List<ResultCategoryResponseDto>> result = _categoryService.TGetAll();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetAll()
    {
        Response<List<ResultCategoryResponseDto>> result = await _categoryService.TGetAllAsync();
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetById(int id)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TGetById(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetById(int id)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TGetByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetByCategoryNo(short categoryNo)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TGetByFilter(x => x.CategoryNo == categoryNo);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetByCategoryNo(short categoryNo)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TGetByFilterAsync(x => x.CategoryNo == categoryNo);
        return ActionResultInstance(result);
    }
    [HttpGet]
    public IActionResult GetByName(string name)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TGetByFilter(x => x.Name.ToLower() == name.ToLower());
        return ActionResultInstance(result);
    }
    [HttpGet]
    public async Task<IActionResult> AsyncGetByName(string name)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TGetByFilterAsync(x => x.Name.ToLower() == name.ToLower());
        return ActionResultInstance(result);
    }
    [HttpPost]
    public IActionResult Create(CreateCategoryRequestDto createCategoryRequestDto)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TCreate(createCategoryRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPost]
    public async Task<IActionResult> AsyncCreate(CreateCategoryRequestDto createCategoryRequestDto)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TCreateAsync(createCategoryRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public IActionResult Update(UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TUpdate(updateCategoryRequestDto);
        return ActionResultInstance(result);
    }
    [HttpPut]
    public async Task<IActionResult> AsyncUpdate(UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TUpdateAsync(updateCategoryRequestDto);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteById(int id)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TDeleteById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteById(int id)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TDeleteByIdAsync(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteByCategoryNo(short categoryNo)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TDeleteByCategoryNo(categoryNo);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteByCategoryNo(short categoryNo)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TDeleteByCategoryNoAsync(categoryNo);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabaseByCategoryNo(short categoryNo)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TDeleteFromDatabaseByCategoryNo(categoryNo);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabaseByCategoryNo(short categoryNo)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TDeleteFromDatabaseByCategoryNoAsync(categoryNo);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public IActionResult DeleteFromDatabaseById(int id)
    {
        Response<ResultCategoryResponseDto> result = _categoryService.TDeleteFromDatabaseById(id);
        return ActionResultInstance(result);
    }
    [HttpDelete]
    public async Task<IActionResult> AsyncDeleteFromDatabaseById(int id)
    {
        Response<ResultCategoryResponseDto> result = await _categoryService.TDeleteFromDatabaseByIdAsync(id);
        return ActionResultInstance(result);
    }
}
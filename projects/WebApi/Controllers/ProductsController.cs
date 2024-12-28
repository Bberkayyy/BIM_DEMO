using BusinessLogicLayer.Abstract;
using Core.Shared;
using EntityLayer.Dtos.RequestDtos.ProductRequestDtos;
using EntityLayer.Dtos.ResponseDtos.ProductResponseDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Response<List<ResultProductResponseDto>> result = _productService.TGetAll();
            return ActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> AsyncGetAll()
        {
            Response<List<ResultProductResponseDto>> result = await _productService.TGetAllAsync();
            return ActionResultInstance(result);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            Response<ResultProductResponseDto> result = _productService.TGetById(id);
            return ActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> AsyncGetById(int id)
        {
            Response<ResultProductResponseDto> result = await _productService.TGetByIdAsync(id);
            return ActionResultInstance(result);
        }
        [HttpGet]
        public IActionResult GetByBarcodeNo(long barcodeNo)
        {
            Response<ResultProductResponseDto> result = _productService.TGetByFilter(x => x.BarcodeNo == barcodeNo);
            return ActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> AsyncGetByBarcodeNo(long barcodeNo)
        {
            Response<ResultProductResponseDto> result = await _productService.TGetByFilterAsync(x => x.BarcodeNo == barcodeNo);
            return ActionResultInstance(result);
        }
        [HttpGet]
        public IActionResult GetByShortCode(int shortCode)
        {
            Response<ResultProductResponseDto> result = _productService.TGetByFilter(x => x.ShortCode == shortCode);
            return ActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> AsyncGetByShortCode(int shortCode)
        {
            Response<ResultProductResponseDto> result = await _productService.TGetByFilterAsync(x => x.ShortCode == shortCode);
            return ActionResultInstance(result);
        }
        [HttpGet]
        public IActionResult GetByName(string name)
        {
            Response<ResultProductResponseDto> result = _productService.TGetByFilter(x => x.Name.ToLower() == name.ToLower());
            return ActionResultInstance(result);
        }
        [HttpGet]
        public async Task<IActionResult> AsyncGetByName(string name)
        {
            Response<ResultProductResponseDto> result = await _productService.TGetByFilterAsync(x => x.Name.ToLower() == name.ToLower());
            return ActionResultInstance(result);
        }
        [HttpPost]
        public IActionResult Create(CreateProductRequestDto createProductRequestDto)
        {
            Response<ResultProductResponseDto> result = _productService.TCreate(createProductRequestDto);
            return ActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> AsyncCreate(CreateProductRequestDto createProductRequestDto)
        {
            Response<ResultProductResponseDto> result = await _productService.TCreateAsync(createProductRequestDto);
            return ActionResultInstance(result);
        }
        [HttpPut]
        public IActionResult Update(UpdateProductRequestDto updateProductRequestDto)
        {
            Response<ResultProductResponseDto> result = _productService.TUpdate(updateProductRequestDto);
            return ActionResultInstance(result);
        }
        [HttpPut]
        public async Task<IActionResult> AsyncUpdate(UpdateProductRequestDto updateProductRequestDto)
        {
            Response<ResultProductResponseDto> result = await _productService.TUpdateAsync(updateProductRequestDto);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            Response<ResultProductResponseDto> result = _productService.TDeleteById(id);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public async Task<IActionResult> AsyncDeleteById(int id)
        {
            Response<ResultProductResponseDto> result = await _productService.TDeleteByIdAsync(id);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public IActionResult DeleteByShortCode(int shortCode)
        {
            Response<ResultProductResponseDto> result = _productService.TDeleteByShortCode(shortCode);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public async Task<IActionResult> AsyncDeleteByShortCode(int shortCode)
        {
            Response<ResultProductResponseDto> result = await _productService.TDeleteByShortCodeAsync(shortCode);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public IActionResult DeleteByBarcodeNo(long barcodeNo)
        {
            Response<ResultProductResponseDto> result = _productService.TDeleteByBarcodeNo(barcodeNo);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public async Task<IActionResult> AsyncDeleteByBarcodeNo(long barcodeNo)
        {
            Response<ResultProductResponseDto> result = await _productService.TDeleteByBarcodeNoAsync(barcodeNo);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public IActionResult DeleteFromDatabaseByBarcodeNo(long barcodeNo)
        {
            Response<ResultProductResponseDto> result = _productService.TDeleteFromDatabaseByBarcodeNo(barcodeNo);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public async Task<IActionResult> AsyncDeleteFromDatabaseByBarcodeNo(long barcodeNo)
        {
            Response<ResultProductResponseDto> result = await _productService.TDeleteFromDatabaseByBarcodeNoAsync(barcodeNo);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public IActionResult DeleteFromDatabaseById(int id)
        {
            Response<ResultProductResponseDto> result = _productService.TDeleteFromDatabaseById(id);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public async Task<IActionResult> AsyncDeleteFromDatabaseById(int id)
        {
            Response<ResultProductResponseDto> result = await _productService.TDeleteFromDatabaseByIdAsync(id);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public IActionResult DeleteFromDatabaseByShortCode(int shortCode)
        {
            Response<ResultProductResponseDto> result = _productService.TDeleteFromDatabaseByShortCode(shortCode);
            return ActionResultInstance(result);
        }
        [HttpDelete]
        public async Task<IActionResult> AsyncDeleteFromDatabaseByShortCode(int shortCode)
        {
            Response<ResultProductResponseDto> result = await _productService.TDeleteFromDatabaseByShortCodeAsync(shortCode);
            return ActionResultInstance(result);
        }
    }
}

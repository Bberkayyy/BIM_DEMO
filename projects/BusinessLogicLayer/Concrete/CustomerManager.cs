using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.BusinessRules.Abstract;
using Core.Shared;
using DataAccessLayer.Repositories.CustomerRepositories;
using EntityLayer.Dtos.RequestDtos.CustomerRequestDtos;
using EntityLayer.Dtos.ResponseDtos.CustomerResponseDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concrete;

public class CustomerManager : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerRules _rules;

    public CustomerManager(ICustomerRepository customerRepository, ICustomerRules rules)
    {
        _customerRepository = customerRepository;
        _rules = rules;
    }

    public Response<ResultCustomerResponseDto> TCreate(CreateCustomerRequestDto createCustomerRequestDto)
    {
        try
        {
            _rules.IdentityNumberMustBeDigitAndElevenCharacter(createCustomerRequestDto.IdentityNumber);
            _rules.PhoneNumberMustBeDigitAndTenCharacter(createCustomerRequestDto.PhoneNumber);
            Customer createCustomer = CreateCustomerRequestDto.ConvertToEntity(createCustomerRequestDto);
            Customer createdCustomer = _customerRepository.Create(createCustomer);
            ResultCustomerResponseDto response = ResultCustomerResponseDto.ConvertToResponse(createdCustomer);
            return new Response<ResultCustomerResponseDto>
            {
                Data = response,
                Message = "Customer created successfully",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCustomerResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<Response<ResultCustomerResponseDto>> TCreateAsync(CreateCustomerRequestDto createCustomerRequestDto)
    {
        try
        {
            _rules.IdentityNumberMustBeDigitAndElevenCharacter(createCustomerRequestDto.IdentityNumber);
            _rules.PhoneNumberMustBeDigitAndTenCharacter(createCustomerRequestDto.PhoneNumber);
            Customer createCustomer = CreateCustomerRequestDto.ConvertToEntity(createCustomerRequestDto);
            Customer createdCustomer = await _customerRepository.CreateAsync(createCustomer);
            ResultCustomerResponseDto response = ResultCustomerResponseDto.ConvertToResponse(createdCustomer);
            return new Response<ResultCustomerResponseDto>
            {
                Data = response,
                Message = "Customer created successfully",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCustomerResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<ResultCustomerResponseDto>> TGetAll(Expression<Func<Customer, bool>>? predicate = null, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null)
    {
        List<Customer> customers = _customerRepository.GetAll(predicate, include);
        List<ResultCustomerResponseDto> response = customers.Select(x => ResultCustomerResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultCustomerResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public async Task<Response<List<ResultCustomerResponseDto>>> TGetAllAsync(Expression<Func<Customer, bool>>? predicate = null, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null)
    {
        List<Customer> customers = await _customerRepository.GetAllAsync(predicate, include);
        List<ResultCustomerResponseDto> response = customers.Select(x => ResultCustomerResponseDto.ConvertToResponse(x)).ToList();
        return new Response<List<ResultCustomerResponseDto>>
        {
            Data = response,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ResultCustomerResponseDto> TGetByFilter(Expression<Func<Customer, bool>> predicate, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null)
    {
        try
        {
            Customer? customer = _customerRepository.GetByFilter(predicate, include);
            _rules.CustomerExists(customer);
            ResultCustomerResponseDto response = ResultCustomerResponseDto.ConvertToResponse(customer!);
            return new Response<ResultCustomerResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCustomerResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultCustomerResponseDto>> TGetByFilterAsync(Expression<Func<Customer, bool>> predicate, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null)
    {
        try
        {
            Customer? customer = await _customerRepository.GetByFilterAsync(predicate, include);
            _rules.CustomerExists(customer);
            ResultCustomerResponseDto response = ResultCustomerResponseDto.ConvertToResponse(customer!);
            return new Response<ResultCustomerResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCustomerResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public Response<ResultCustomerResponseDto> TGetById(int id, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null)
    {
        try
        {
            Customer? customer = _customerRepository.GetById(id, include);
            _rules.CustomerExists(customer);
            ResultCustomerResponseDto response = ResultCustomerResponseDto.ConvertToResponse(customer!);
            return new Response<ResultCustomerResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCustomerResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }

    public async Task<Response<ResultCustomerResponseDto>> TGetByIdAsync(int id, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null)
    {
        try
        {
            Customer? customer = await _customerRepository.GetByIdAsync(id, include);
            _rules.CustomerExists(customer);
            ResultCustomerResponseDto response = ResultCustomerResponseDto.ConvertToResponse(customer!);
            return new Response<ResultCustomerResponseDto>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new Response<ResultCustomerResponseDto>
            {
                Message = e.Message,
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
        }
    }
}

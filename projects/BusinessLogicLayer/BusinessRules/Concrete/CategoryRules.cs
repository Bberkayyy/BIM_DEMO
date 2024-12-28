using BusinessLogicLayer.BusinessRules.Abstract;
using Core.CrossCuttingConcerns;
using DataAccessLayer.Repositories.CategoryRepositories;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Concrete;

public class CategoryRules : ICategoryRules
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRules(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void CategoryExists(Category? category, bool isDeleteFromDatabase = false)
    {
        if (category == null)
            throw new BusinessException("Category not found!");
        if (!isDeleteFromDatabase && category.Deleted != null)
            throw new BusinessException($"Category not found! Category deleted at {category.Deleted?.ToString("yyyy-MM-dd HH:mm")}.");
    }

    public void CategoryNameCanNotBeNullOrWhiteSpace(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
            throw new BusinessException("Please enter a category name!");
    }

    public void CategoryNameMustBeUnique(string categoryName)
    {
        Category? category = _categoryRepository.GetByFilter(x => x.Name.ToLower() == categoryName.ToLower());
        if (category != null)
            throw new BusinessException($"Category name is already exists! ({categoryName}). Enter a different category name.");
    }

    public void CategoryNoMustBeThreeCharacter(short categoryNo)
    {
        if (categoryNo < 100 || categoryNo > 999)
            throw new BusinessException($"Category no must be between 100 and 999. Please enter a number in this range. ({categoryNo})");
    }

    public void CategoryNoMustBeUnique(short categoryNo, int id = -1)
    {
        Category? category = _categoryRepository.GetByFilter(x => x.CategoryNo == categoryNo);
        if (category != null && category.Id != id)
        {
            if (category.CategoryNo == categoryNo)
                throw new BusinessException($"Category no is already exists! ({categoryNo}). Enter a different category no.");
            if (category.Id != id)
                throw new BusinessException($"Category id cannot be changed!");
        }

    }

    public bool CategoryNoMustBeUnique(short categoryNo)
    {
        return _categoryRepository.EntityExists(x => x.CategoryNo == categoryNo);
    }
}

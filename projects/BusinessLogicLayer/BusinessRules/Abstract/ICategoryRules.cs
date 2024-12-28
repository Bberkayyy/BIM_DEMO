using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessRules.Abstract;

public interface ICategoryRules
{
    void CategoryNoMustBeUnique(short categoryNo, int id = -1);
    bool CategoryNoMustBeUnique(short categoryNo);
    void CategoryNoMustBeThreeCharacter(short categoryNo);
    void CategoryNameMustBeUnique(string categoryName);
    void CategoryExists(Category? category, bool isDeleteFromDatabase = false);
    void CategoryNameCanNotBeNullOrWhiteSpace(string categoryName);
}

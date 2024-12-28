using Core.Persistence.DtoBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer;

public record CreateCategoryDto : IRequestDto
{
    public string Name { get; set; } = null!;
}

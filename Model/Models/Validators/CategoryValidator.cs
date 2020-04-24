using FluentValidation;
using MemeSite.Data.DbContext;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MemeSite.Data.Repository;
using System.Threading.Tasks;

namespace MemeSite.Data.Models.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(m => m.CategoryName)
                .NotEmpty().WithMessage("Category name required")
                .MaximumLength(14).WithMessage("Maximum length is 14");
        }

    }
}

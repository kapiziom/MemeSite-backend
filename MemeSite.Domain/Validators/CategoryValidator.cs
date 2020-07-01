using FluentValidation;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Domain.Validators
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

using FluentValidation;
using Vb.Schema;

namespace Vb.Business.Validator
{
    public class AccountValidator : AbstractValidator<AccountRequest>
    {
        public AccountValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
            RuleFor(x => x.IBAN).NotEmpty().MaximumLength(34);
            RuleFor(x => x.Balance).NotEmpty();
            RuleFor(x => x.CurrencyType).NotEmpty().MaximumLength(3);
            
        }
    }
}

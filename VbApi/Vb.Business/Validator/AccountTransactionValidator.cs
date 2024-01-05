using FluentValidation;
using Vb.Schema;

namespace Vb.Business.Validator
{
    public class AccountTransactionValidator : AbstractValidator<AccountTransactionRequest>
    {
        public AccountTransactionValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.ReferenceNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.TransactionDate).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.TransferType).NotEmpty().MaximumLength(10);

        }
    }
}

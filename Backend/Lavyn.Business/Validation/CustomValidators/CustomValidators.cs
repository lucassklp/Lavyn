using FluentValidation;

namespace Lavyn.Business.Validators.CustomValidators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, TProperty> CPF<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CPFValidator());
        }
    }
}

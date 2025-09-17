using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Application;

public class PersianIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateUserName(string userName)
        => new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = $"کاربری با این شماره تلفن '{userName}' قبلاً ثبت شده است."
        };

    public override IdentityError DuplicateEmail(string email)
        => new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = $"ایمیل '{email}' قبلاً ثبت شده است."
        };

    public override IdentityError InvalidUserName(string userName)
        => new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = $"نام کاربری '{userName}' نامعتبر است."
        };

    public override IdentityError PasswordTooShort(int length)
        => new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = $"رمز عبور باید حداقل {length} کاراکتر باشد."
        };

    public override IdentityError PasswordRequiresNonAlphanumeric()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "رمز عبور باید شامل حداقل یک کاراکتر غیرحرفی (مثل @، #، ! و...) باشد."
        };

    public override IdentityError PasswordRequiresDigit()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "رمز عبور باید حداقل شامل یک عدد باشد."
        };

    public override IdentityError PasswordRequiresUpper()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "رمز عبور باید حداقل شامل یک حرف بزرگ لاتین باشد."
        };

    public override IdentityError PasswordRequiresLower()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "رمز عبور باید حداقل شامل یک حرف کوچک لاتین باشد."
        };
}
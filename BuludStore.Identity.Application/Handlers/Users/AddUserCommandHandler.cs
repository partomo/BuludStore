// using AutoMapper;
// using Bazta.Application.Interfaces;
// using Bazta.Identity.Application.Commands.Users;
// using Bazta.Identity.Application.DTOs;
// using Bazta.Identity.Application.Extensions;
// using Bazta.Identity.Domain.Entities;
// using Bulud.Base.Exceptions;
// using MediatR;
// using Microsoft.AspNetCore.Identity;
//
// namespace Bazta.Identity.Application.Handlers.Users;
//
// public class AddUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper, ICurrentUserContext currentUser)
//     : IRequestHandler<AddUserCommand, UserDto>
// {
//     public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
//     {
//         if (currentUser.IsCountyAdmin())
//         {
//             if (request.CountyId != currentUser.CountyId && request.ProvinceId != currentUser.ProvinceId)
//                 throw new ForbiddenException("شما فقط می توانید برای شهرستان خود کاربر ایجاد کنید.");
//         }
//         else if (currentUser.IsProvinceAdmin())
//         {
//             if (request.ProvinceId != currentUser.ProvinceId)
//                 throw new ForbiddenException("شما فقط می توانید برای استان خود کاربر ایجاد کنید.");
//         }
//         
//         var user = mapper.Map<AppUser>(request);
//         user.UserName = request.PhoneNumber;
//         var result = await userManager.CreateAsync(user, request.Password);
//         if (result.Succeeded)
//         {
//             await userManager.AddToRolesAsync(user, new List<string> { request.Role });
//             return mapper.Map<UserDto>(user);
//         }
//         
//         throw new AppValidationException(result.ToErrorDictionary());
//     }
// }

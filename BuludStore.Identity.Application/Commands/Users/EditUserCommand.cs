using System.ComponentModel.DataAnnotations;
using BuludStore.Identity.Application.DTOs;
using MediatR;

namespace BuludStore.Identity.Application.Commands.Users;

public class EditUserCommand : IRequest<UserDto>
{
    [Required]
    public required string Id { get; set; }
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public required string PhoneNumber { get; set; }
    public string? Password { get; set; }
    [Required]
    public required string NationalCode { get; set; }
    public int? ProvinceId { get; set; }
    public int? CountyId { get; set; }
    [Required]
    public required string Role { get; set; }
    public string? Description { get; set; }
}
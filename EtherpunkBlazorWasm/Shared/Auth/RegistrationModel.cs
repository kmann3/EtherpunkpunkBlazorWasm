using System.ComponentModel.DataAnnotations;

namespace EtherpunkBlazorWasm.Shared;
public class RegistrationModel : LoginModel
{
    [Required(ErrorMessage = "Confirm password is required.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
    public string ConfirmPassword { get; set; }
}

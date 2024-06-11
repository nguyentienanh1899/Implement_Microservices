using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
    }
}

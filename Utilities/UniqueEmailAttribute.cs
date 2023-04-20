# pragma warning disable CS8600
using System.ComponentModel.DataAnnotations;
using BJHRApp.Data;
namespace BJHRApp.Utilities;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("Field required");
        }
        //Inject the DB
        DBContext _context = (DBContext)validationContext.GetService(typeof(DBContext))!;
        //Query the DB to see if the email exists
        if(_context.Users.Any(e => e.Email == value.ToString()))
        {
            return new ValidationResult("Email must be unique");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}
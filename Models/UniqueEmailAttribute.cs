//TODO: message-Brian Not sure where to put custom validations/annotations, but figured this could live here for now
using System.ComponentModel.DataAnnotations;
namespace BJHRApp.Models;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("Email is required!");
        }
        //TODO: message-Brian If these comments are silly I can just remove them. Moreso making syntax notes for myself. 😅 
        //Also, can emojis be in comments? Like, for fun?
        //Inject the DB
        DBContext _context = (DBContext)validationContext.GetService(typeof(DBContext));
        //Query the DB to see if the email exists
        if(_context.Users.Any(e => e.Email == value.ToString()))
        {
            return new ValidationResult("Email must be unique!");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}
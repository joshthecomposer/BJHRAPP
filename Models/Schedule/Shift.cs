namespace BJHRApp.Models;

public class Shift : BaseEntity
{

    public DateTime In {get;set;}
    public DateTime Out {get;set;}

    public int UserId {get;set;}
    public User? User {get;set;}
}
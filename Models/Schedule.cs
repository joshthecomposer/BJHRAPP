namespace BJHRApp.Models;

public class Schedule : BaseEntity
{
    public DateTime In {get;set;}
    public DateTime Out {get;set;}

    public User? User {get;set;}
}
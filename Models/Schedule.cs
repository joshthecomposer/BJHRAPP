namespace BJHRApp.Models;

public class Schedule : BaseEntity
{
    private DateTime In {get;set;}
    private DateTime Out {get;set;}

    public User? User {get;set;}
}
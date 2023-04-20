#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJHRApp.Models;
public class Absence : BaseEntity
{
    public DateTime AbsenceDate { get; set; }
    //NOTE: I propose that absence type be int values: 
        //  0 for unexcused,
        //  1 for PTO
        //  2 for Sick Day
        //  ...extended as needed.
        // this could also just be an enum actually.
    public int AbsenceType { get; set; }

}
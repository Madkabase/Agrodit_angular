using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class AlertsModel
    {
        [Required]
public long Id{get;set;}
[Required]
public DateTime Date{get;set;}
[Required]
public long AlertType{get;set;}
public long? FieldId{get;set;}
[Required]
public bool Closed{get;set;}
    }
}


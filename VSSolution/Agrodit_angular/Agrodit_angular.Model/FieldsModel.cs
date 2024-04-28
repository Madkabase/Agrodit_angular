using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class FieldsModel
    {
        [Required]
public long Id{get;set;}
[Required]
public string Name{get;set;}
[Required]
public long CompanyId{get;set;}
public string Geofence{get;set;}
public long? ThresholdId{get;set;}
    }
}


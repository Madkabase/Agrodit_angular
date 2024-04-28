using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class CompaniesModel
    {
        [Required]
public long Id{get;set;}
[Required]
public string Name{get;set;}
[Required]
public long OwnerId{get;set;}
[Required]
public string AppId{get;set;}
[Required]
public string AppName{get;set;}
[Required]
public long MaxDevices{get;set;}
[Required]
public string CompanyType{get;set;}
    }
}


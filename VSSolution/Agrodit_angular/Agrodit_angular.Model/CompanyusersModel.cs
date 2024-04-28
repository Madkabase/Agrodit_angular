using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class CompanyusersModel
    {
        [Required]
public long Id{get;set;}
[Required]
public long UserId{get;set;}
[Required]
public long CompanyId{get;set;}
[Required]
public long CompanyRole{get;set;}
[Required]
public long FieldId{get;set;}
    }
}


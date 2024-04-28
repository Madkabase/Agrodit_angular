using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class UsersModel
    {
        [Required]
public long Id{get;set;}
[Required]
public string FirstName{get;set;}
[Required]
public string LastName{get;set;}
[Required]
public string Email{get;set;}
[Required]
public string Password{get;set;}
[Required]
public bool IsVerified{get;set;}
[Required]
public long ConfirmationCode{get;set;}
[Required]
public DateTime ConfirmationExpirationDate{get;set;}
[Required]
public long ConfirmationTriesCounter{get;set;}
    }
}


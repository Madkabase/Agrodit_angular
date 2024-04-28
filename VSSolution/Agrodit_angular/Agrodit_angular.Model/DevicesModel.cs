using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class DevicesModel
    {
        [Required]
public string Name{get;set;}
[Required]
public string DevEUI{get;set;}
[Required]
public string JoinEUI{get;set;}
[Required]
public string AppKey{get;set;}
[Required]
public long FieldId{get;set;}
public long? CalibrationMoisture1Max{get;set;}
public long? CalibrationMoisture1Min{get;set;}
public long? CalibrationMoisture2Max{get;set;}
public long? CalibrationMoisture2Min{get;set;}
public long? CalibrationSalinity1Max{get;set;}
public long? CalibrationSalinity1Min{get;set;}
public long? CalibrationSalinity2Max{get;set;}
public long? CalibrationSalinity2Min{get;set;}
public double? Location{get;set;}
[Required]
public long Id{get;set;}
public string Status{get;set;}
public long? CompanyId{get;set;}
    }
}


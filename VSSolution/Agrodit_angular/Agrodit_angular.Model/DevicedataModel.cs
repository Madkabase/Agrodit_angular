using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class DevicedataModel
    {
        [Required]
public long Id{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Moisture1{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Moisture2{get;set;}
public long? BatteryLevel{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Temperature1{get;set;}
[Required]
public DateTime TimeStamp{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Temperature2{get;set;}
[Required]
public long Salinity1{get;set;}
[Required]
public long Salinity2{get;set;}
public long? DeviceId{get;set;}
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class GlobalthresholdpresetsModel
    {
        [Required]
public long Id{get;set;}
[Required]
public string Name{get;set;}
[Required]
public long Moisture1Min{get;set;}
[Required]
public long Moisture1Max{get;set;}
[Required]
public long Moisture2Min{get;set;}
[Required]
public long Moisture2Max{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Temperature1Min{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Temperature1Max{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Temperature2Max{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Temperature2Min{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Salinity1Max{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Salinity1Min{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Salinity2Max{get;set;}
[Range(double.MinValue,double.MaxValue)]
public double Salinity2Min{get;set;}
    }
}


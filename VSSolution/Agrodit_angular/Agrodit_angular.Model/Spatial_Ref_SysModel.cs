using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Agrodit_angular.Model
{
    public class Spatial_Ref_SysModel
    {
        [Required]
public long srid{get;set;}
public string auth_name{get;set;}
public long? auth_srid{get;set;}
public string srtext{get;set;}
public string proj4text{get;set;}
    }
}


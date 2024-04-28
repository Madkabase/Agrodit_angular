using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface IDevicesManager
    {
        APIResponse GetDevices(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchDevices(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterDevices(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetDevicesByID(int id);
        APIResponse UpdateDevices(int id,DevicesModel model);
        APIResponse AddDevices(DevicesModel model);
		APIResponse DeleteDevices(int id);
        APIResponse DeleteMultipleDevices(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


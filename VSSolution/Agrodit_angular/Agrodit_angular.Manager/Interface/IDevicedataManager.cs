using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface IDevicedataManager
    {
        APIResponse GetDevicedata(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchDevicedata(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterDevicedata(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetDevicedataByID(int id);
        APIResponse UpdateDevicedata(int id,DevicedataModel model);
        APIResponse AddDevicedata(DevicedataModel model);
		APIResponse DeleteDevicedata(int id);
        APIResponse DeleteMultipleDevicedata(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface IDevicesDataAccess
    {
        List<DevicesModel> GetAllDevices(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<DevicesModel> SearchDevices(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<DevicesModel> FilterDevices(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        DevicesModel GetDevicesByID(int id);
        bool UpdateDevices(DevicesModel model);
        int GetAllTotalRecordDevices();
        int GetSearchTotalRecordDevices(string searchKey);
        int GetFilterTotalRecordDevices(List<FilterModel> filterBy,string andOr);
        long AddDevices(DevicesModel model);
        bool DeleteDevices(int id);
        bool DeleteMultipleDevices(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


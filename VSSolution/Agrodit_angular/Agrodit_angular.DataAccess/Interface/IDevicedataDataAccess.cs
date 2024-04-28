using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface IDevicedataDataAccess
    {
        List<DevicedataModel> GetAllDevicedata(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<DevicedataModel> SearchDevicedata(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<DevicedataModel> FilterDevicedata(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        DevicedataModel GetDevicedataByID(int id);
        bool UpdateDevicedata(DevicedataModel model);
        int GetAllTotalRecordDevicedata();
        int GetSearchTotalRecordDevicedata(string searchKey);
        int GetFilterTotalRecordDevicedata(List<FilterModel> filterBy,string andOr);
        long AddDevicedata(DevicedataModel model);
        bool DeleteDevicedata(int id);
        bool DeleteMultipleDevicedata(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


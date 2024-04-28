using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface IFieldsDataAccess
    {
        List<FieldsModel> GetAllFields(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<FieldsModel> SearchFields(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<FieldsModel> FilterFields(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        FieldsModel GetFieldsByID(int id);
        bool UpdateFields(FieldsModel model);
        int GetAllTotalRecordFields();
        int GetSearchTotalRecordFields(string searchKey);
        int GetFilterTotalRecordFields(List<FilterModel> filterBy,string andOr);
        long AddFields(FieldsModel model);
        bool DeleteFields(int id);
        bool DeleteMultipleFields(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


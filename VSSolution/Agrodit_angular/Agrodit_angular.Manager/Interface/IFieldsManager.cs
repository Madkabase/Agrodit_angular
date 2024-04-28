using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agrodit_angular.Manager.Interface
{
    public interface IFieldsManager
    {
        APIResponse GetFields(int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse SearchFields(string searchKey, int page, int itemsPerPage,List<OrderByModel> orderBy);
        APIResponse FilterFields(List<FilterModel> filterModels, string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        APIResponse GetFieldsByID(int id);
        APIResponse UpdateFields(int id,FieldsModel model);
        APIResponse AddFields(FieldsModel model);
		APIResponse DeleteFields(int id);
        APIResponse DeleteMultipleFields(List<DeleteMultipleModel> deleteParam,string andOr);
    }
}


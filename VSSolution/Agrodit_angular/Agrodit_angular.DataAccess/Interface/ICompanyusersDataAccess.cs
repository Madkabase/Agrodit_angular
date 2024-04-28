using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface ICompanyusersDataAccess
    {
        List<CompanyusersModel> GetAllCompanyusers(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<CompanyusersModel> SearchCompanyusers(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<CompanyusersModel> FilterCompanyusers(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        CompanyusersModel GetCompanyusersByID(int id);
        bool UpdateCompanyusers(CompanyusersModel model);
        int GetAllTotalRecordCompanyusers();
        int GetSearchTotalRecordCompanyusers(string searchKey);
        int GetFilterTotalRecordCompanyusers(List<FilterModel> filterBy,string andOr);
        long AddCompanyusers(CompanyusersModel model);
        bool DeleteCompanyusers(int id);
        bool DeleteMultipleCompanyusers(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


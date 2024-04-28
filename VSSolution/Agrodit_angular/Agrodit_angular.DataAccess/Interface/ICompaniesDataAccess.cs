using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System;
using System.Collections.Generic;

namespace Agrodit_angular.DataAccess.Interface
{
    public interface ICompaniesDataAccess
    {
        List<CompaniesModel> GetAllCompanies(int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<CompaniesModel> SearchCompanies(string searchKey,int page, int itemsPerPage,List<OrderByModel> orderBy);
        List<CompaniesModel> FilterCompanies(List<FilterModel> filterModels,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy);
        CompaniesModel GetCompaniesByID(int id);
        bool UpdateCompanies(CompaniesModel model);
        int GetAllTotalRecordCompanies();
        int GetSearchTotalRecordCompanies(string searchKey);
        int GetFilterTotalRecordCompanies(List<FilterModel> filterBy,string andOr);
        long AddCompanies(CompaniesModel model);
        bool DeleteCompanies(int id);
        bool DeleteMultipleCompanies(List<DeleteMultipleModel> deleteParam,string andOr);
        
        
    }
}


import { Component, OnInit, ViewChild } from '@angular/core';
import { ICompanyusers } from '../../../interface/ICompanyusers';
import { SelectionModel } from '@angular/cdk/collections';
import { CompanyusersService } from '../../../service/CompanyUsers.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ColDef, FilterChangedEvent, GridReadyEvent,  IDatasource, IGetRowsParams, ITextFilterParams, PaginationChangedEvent, RowModelType} from 'ag-grid-community';
import { AgGridAngular } from 'ag-grid-angular';
import { convertStringToFilterCondition, filterCondtionArray, IFilterCondition } from 'src/app/interface/FilterConditionEnum';
import { ButtonRendererComponent } from 'src/app/ag-components/button-renderer.component';


@Component({
  selector: 'app-CompanyUsers',
  templateUrl: './CompanyUsers.component.html',
  styleUrls: ['./CompanyUsers.component.scss']
})
export class CompanyusersComponent implements OnInit {
  public frameworkComponents: any;
  public defaultColDef: ColDef = {
    editable: false,
    sortable: true,
    resizable: true,
    filter: true,
    flex: 1,
    minWidth: 100,
  };
  public rowSelection: 'single';
  public rowModelType: RowModelType = 'infinite';
  public cacheBlockSize = 20;
  public cacheOverflowSize = 2;
  public maxConcurrentDatasourceRequests = 2;
  public infiniteInitialRowCount = 1;
  public maxBlocksInCache = 2;
  public editType: 'fullRow' = 'fullRow';
  public filterParams = {
    filterOptions: filterCondtionArray,
    defaultOption: 'Equals',
  }
  public columnDefs: ColDef[] = [
  {
    headerName: 'Edit',
    cellRenderer: 'buttonRenderer',
    editable: false,
    sortable:false,
    suppressFiltersToolPanel:true,
    maxWidth:100,
    filter: false,
    cellRendererParams: {
    onClick: this.editItem.bind(this),
    label: 'Edit',
    class: 'action-button edit'
    },
  },
  {
    headerName: 'Delete',
    cellRenderer: 'buttonRenderer',
    editable: false,
    sortable:false,
    maxWidth:120,
    suppressFiltersToolPanel:true,
    filter: false,
    cellRendererParams: {
    onClick: this.deleteItem.bind(this),
    label: 'Delete',
    class: 'action-button delete'
    },
  },
    { field: 'Id', editable: false, filterParams: this.filterParams as ITextFilterParams },
{ field: 'UserId', editable: false, filterParams: this.filterParams as ITextFilterParams },
{ field: 'CompanyId', editable: false, filterParams: this.filterParams as ITextFilterParams },
{ field: 'CompanyRole', editable: false, filterParams: this.filterParams as ITextFilterParams },
{ field: 'FieldId', editable: false, filterParams: this.filterParams as ITextFilterParams },

    ];

  dataSource: ICompanyusers[];
  selection: SelectionModel<ICompanyusers>;
  pageNo: number;
  orderBy: string;
  pageSize: number;
  searchValue: string;
  totalRecord: number;
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  constructor(private CompanyUsersService: CompanyusersService, private router: Router, private toastr: ToastrService) { 
    this.frameworkComponents = {
      buttonRenderer: ButtonRendererComponent,
    }
  }

  ngOnInit() {
    this.pageNo = 0;
    this.pageSize = 20;
    this.searchValue = '';
    this.totalRecord = 0;
    this.orderBy="";
  }
  restoreFilterModel() {
    this.pageNo = 0;
    this.pageSize = 20;
    this.searchValue = '';
    this.totalRecord = 0;
    this.orderBy="";
    this.agGrid.api.setFilterModel({});
  }
  onFilterChanged(params: FilterChangedEvent<ICompanyusers>) {
    let filterModel = this.agGrid.api.getFilterModel();
    let filterArray: IFilterCondition[] = [];
    let orAnd = "OR";
    Object.keys(filterModel).forEach(key => {
      if (filterModel[key].operator) {
        orAnd = filterModel[key].operator;
        let conditionObj = filterModel[key];
        Object.keys(conditionObj).forEach(cKey => {
          if (conditionObj[cKey].filter) {
            let condtion: IFilterCondition = {
              ColumnName: key,
              ColumnCondition: convertStringToFilterCondition(conditionObj[cKey].type),
              ColumnValue: conditionObj[cKey].filter
            }
            filterArray.push(condtion);
          }
        });
      } else {
        let condtion: IFilterCondition = {
          ColumnName: key,
          ColumnCondition: convertStringToFilterCondition(filterModel[key].type),
          ColumnValue: filterModel[key].filter
        }
        filterArray.push(condtion);
      }

    });
    if (filterArray.length > 0) {
      const dataSource: IDatasource = {
        rowCount: undefined,
        getRows: (paramRow: IGetRowsParams) => {
          this.pageNo = Math.ceil(paramRow.startRow / this.pageSize);
          this.CompanyUsersService.filterCompanyusers(filterArray,this.pageNo + 1, this.pageSize, orAnd, this.orderBy).then((res: any) => {
            if (res.code === 1) {
              paramRow.successCallback(res.document.records, res.document.totalRecords);
            }
          });
        },
      };
      params.api.setDatasource(dataSource);
    } else {
      this.getData(params);
    }
  }
  onGridReady(params: GridReadyEvent) {
    this.agGrid.api.setDomLayout('autoHeight');
    this.getData(params);
  }
  onPaginationChanged(params: PaginationChangedEvent<ICompanyusers>) {
    this.pageNo = params.api.paginationGetCurrentPage();
    this.CompanyUsersService.getCompanyusers(params.api.paginationGetCurrentPage() + 1, this.pageSize, this.orderBy).then((res: any) => {
      if (res.code === 1) {
        params.api.setRowData(res.document.records)
        params.api.setRowCount(res.document.totalRecords)
      }
    })
  }
  getData(params) {
    const dataSource: IDatasource = {
      rowCount: undefined,
      getRows: (paramRow: IGetRowsParams) => {
        this.pageNo = Math.ceil(paramRow.startRow / this.pageSize);
        this.CompanyUsersService.getCompanyusers(this.pageNo + 1, this.pageSize, "",this.orderBy).then((res: any) => {
          if (res.code === 1) {
            paramRow.successCallback(res.document.records, res.document.totalRecords);
          }
        });
      },
    };
    params.api.setDatasource(dataSource);
  }
  onBtExport() {
    this.agGrid.api.exportDataAsExcel();
  }
  deleteItem(row: any) {
    if (confirm("Are you sure, you want to delete?")) {
      this.CompanyUsersService.deleteCompanyusers(row.Id).then((res: any) => {
        if (res.code === 1) {
          this.toastr.success(res.message);
          location.reload();
        } else {
          this.toastr.error(res.message);
        }
      })
    }
  }
  editItem(row: any) {
    this.router.navigateByUrl("CompanyUsers/" + row.Id);
  }
  onSortChanged(params){
    const sortBy=params.columnApi.getColumnState().find(s => s.sort != null);
    if(sortBy){
    this.orderBy=sortBy.colId +"|" +sortBy.sort;
    this.getData(params);
    }
  }
}



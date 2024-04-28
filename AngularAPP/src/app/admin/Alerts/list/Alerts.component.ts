import { Component, OnInit, ViewChild } from '@angular/core';
import { IAlerts } from '../../../interface/IAlerts';
import { SelectionModel } from '@angular/cdk/collections';
import { AlertsService } from '../../../service/Alerts.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ColDef, FilterChangedEvent, GridReadyEvent,  IDatasource, IGetRowsParams, ITextFilterParams, PaginationChangedEvent, RowModelType} from 'ag-grid-community';
import { AgGridAngular } from 'ag-grid-angular';
import { convertStringToFilterCondition, filterCondtionArray, IFilterCondition } from 'src/app/interface/FilterConditionEnum';
import { ButtonRendererComponent } from 'src/app/ag-components/button-renderer.component';


@Component({
  selector: 'app-Alerts',
  templateUrl: './Alerts.component.html',
  styleUrls: ['./Alerts.component.scss']
})
export class AlertsComponent implements OnInit {
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
{ field: 'Date', editable: false, filterParams: this.filterParams as ITextFilterParams },
{ field: 'AlertType', editable: false, filterParams: this.filterParams as ITextFilterParams },
{ field: 'FieldId', editable: false, filterParams: this.filterParams as ITextFilterParams },
{ field: 'Closed', editable: false, filterParams: this.filterParams as ITextFilterParams },

    ];

  dataSource: IAlerts[];
  selection: SelectionModel<IAlerts>;
  pageNo: number;
  orderBy: string;
  pageSize: number;
  searchValue: string;
  totalRecord: number;
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  constructor(private AlertsService: AlertsService, private router: Router, private toastr: ToastrService) { 
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
  onFilterChanged(params: FilterChangedEvent<IAlerts>) {
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
          this.AlertsService.filterAlerts(filterArray,this.pageNo + 1, this.pageSize, orAnd, this.orderBy).then((res: any) => {
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
  onPaginationChanged(params: PaginationChangedEvent<IAlerts>) {
    this.pageNo = params.api.paginationGetCurrentPage();
    this.AlertsService.getAlerts(params.api.paginationGetCurrentPage() + 1, this.pageSize, this.orderBy).then((res: any) => {
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
        this.AlertsService.getAlerts(this.pageNo + 1, this.pageSize, "",this.orderBy).then((res: any) => {
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
      this.AlertsService.deleteAlerts(row.Id).then((res: any) => {
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
    this.router.navigateByUrl("Alerts/" + row.Id);
  }
  onSortChanged(params){
    const sortBy=params.columnApi.getColumnState().find(s => s.sort != null);
    if(sortBy){
    this.orderBy=sortBy.colId +"|" +sortBy.sort;
    this.getData(params);
    }
  }
}



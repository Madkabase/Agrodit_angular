export const childRoutes = [
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard.module').then(m => m.DashboardModule),
    data: { icon: 'dashboard', text: 'Dashboard' }
  },
  {
    path: 'upload',
    loadChildren: () =>
      import('./upload/upload.module').then(m => m.UploadModule),
    data: { icon: 'upload', text: 'Upload' }
  },
  

































































{   path: 'devicedata',   loadChildren: () =>     import('./devicedata/devicedata.module').then(m => m.DevicedataModule),   data: { icon: 'table_chart', text: 'Devicedata' }},
{   path: 'devices',   loadChildren: () =>     import('./devices/devices.module').then(m => m.DevicesModule),   data: { icon: 'table_chart', text: 'Devices' }},
{   path: 'GlobalThresholdPresets',   loadChildren: () =>     import('./GlobalThresholdPresets/GlobalThresholdPresets.module').then(m => m.GlobalthresholdpresetsModule),   data: { icon: 'table_chart', text: 'Globalthresholdpresets' }},
{   path: 'Thresholds',   loadChildren: () =>     import('./Thresholds/Thresholds.module').then(m => m.ThresholdsModule),   data: { icon: 'table_chart', text: 'Thresholds' }},
{   path: 'Users',   loadChildren: () =>     import('./Users/Users.module').then(m => m.UsersModule),   data: { icon: 'table_chart', text: 'Users' }},
{   path: 'Companies',   loadChildren: () =>     import('./Companies/Companies.module').then(m => m.CompaniesModule),   data: { icon: 'table_chart', text: 'Companies' }},
{   path: 'ThresholdPresets',   loadChildren: () =>     import('./ThresholdPresets/ThresholdPresets.module').then(m => m.ThresholdpresetsModule),   data: { icon: 'table_chart', text: 'Thresholdpresets' }},
{   path: 'Alerts',   loadChildren: () =>     import('./Alerts/Alerts.module').then(m => m.AlertsModule),   data: { icon: 'table_chart', text: 'Alerts' }},
{   path: 'CompanyUsers',   loadChildren: () =>     import('./CompanyUsers/CompanyUsers.module').then(m => m.CompanyusersModule),   data: { icon: 'table_chart', text: 'Companyusers' }},
{   path: 'spatial_ref_sys',   loadChildren: () =>     import('./spatial_ref_sys/spatial_ref_sys.module').then(m => m.Spatial_Ref_SysModule),   data: { icon: 'table_chart', text: 'Spatial_Ref_Sys' }},
{   path: 'Fields',   loadChildren: () =>     import('./Fields/Fields.module').then(m => m.FieldsModule),   data: { icon: 'table_chart', text: 'Fields' }},

];


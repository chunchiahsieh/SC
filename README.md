
#更新MES
Enable-Migrations -ContextTypeName WebApplication1.Models.MES_DbContext -MigrationsDirectory Migrations\MES
Add-Migration AddLogOnTable -ConfigurationTypeName WebApplication1.Migrations.MES.Configuration
Update-Database -ConfigurationTypeName WebApplication1.Migrations.MES.Configuration


#更新ERP
Enable-Migrations -ContextTypeName WebApplication1.Models.ERP_DbContext -MigrationsDirectory Migrations\ERP
Add-Migration AddSomethingToERP -ConfigurationTypeName WebApplication1.Migrations.ERP.Configuration
Update-Database -ConfigurationTypeName WebApplication1.Migrations.ERP.Configuration

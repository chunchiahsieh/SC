
#更新MES
Enable-Migrations -ContextTypeName WebApplication1.Models.MES_DbContext
Add-Migration InitialCreate
Update-Database
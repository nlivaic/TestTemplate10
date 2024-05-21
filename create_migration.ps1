# Usage: migrations_su.ps1 <previous_migration> <next_migration_number> <next_migration_name>
# Usage: migrations_su.ps1 '0001_Initial' '0003_StudycastIntegration'
$previous_migration=$args[0]
$next_migration_name=$args[1]
$full_script_path="../TestTemplate10.Migrations/TestTemplate10Scripts/" + $next_migration_name + ".sql"
cd ./src/TestTemplate10.Data
dotnet ef migrations add $next_migration_name --startup-project ../TestTemplate10.Api/TestTemplate10.Api.csproj --context TestTemplate10DbContext
if ($previous_migration -eq '')
{
    dotnet ef migrations script --startup-project ../TestTemplate10.Api/TestTemplate10.Api.csproj --context TestTemplate10DbContext -o $full_script_path
}
else
{
    dotnet ef migrations script --startup-project ../TestTemplate10.Api/TestTemplate10.Api.csproj --context TestTemplate10DbContext $previous_migration $next_migration_name -o $full_script_path
}
cd ../..
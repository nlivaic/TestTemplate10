param ($sqlUsersGroupName, $resourceGroupName, $appServiceWebName)

####################################################
### Create User Group
####################################################
Write-Host "##[warning]--- Create and Populate User Group - START ---"
$sqlUsersGroupId=(az ad group list --filter "displayName eq '$sqlUsersGroupName'" --query '[].id' --output tsv)
If ($sqlUsersGroupId -eq $null) {
    $sqlUsersGroupId = (az ad group create --display-name $sqlUsersGroupName --mail-nickname $sqlUsersGroupName --query id --output tsv)
    Write-Host "##[section]Created Entra group '$sqlUsersGroupName' with group Id: $sqlUsersGroupId"
}
Write-Host "##[warning]--- Create and Populate User Group - END ---"

####################################################
### Assign roles to User Group
####################################################
Write-Host "##[warning]--- Assign roles to User Group - START ---"
$rgId = az group show --resource-group $resourceGroupName --query id --output tsv
# Scoped to resource group
# Key Vault Secrets Officer role
az role assignment create --assignee $sqlUsersGroupId --role "b86a8fe4-44ce-4948-aee5-eccb2c155cd7" --scope $rgId
Write-Host "##[section]Added Entra group '$sqlUsersGroupId' to role 'Key Vault Secrets Officer'"
Write-Host "##[warning]--- Assign roles to User Group - END ---"

####################################################
### Create Managed Identity for Web API
####################################################
Write-Host "##[warning]--- Create Managed Identity for Web API - START ---"
# Enable managed identity on app
$managedIdentityId = (az webapp identity show --resource-group $resourceGroupName --name $appServiceWebName --query principalId --output tsv)
If ($managedIdentityId -eq $null) {
    $managedIdentityId = (az webapp identity assign --resource-group $resourceGroupName --name $appServiceWebName  --query principalId --output tsv)
    Write-Host "##[section]Created system-assigned managed identity for '$appServiceWebName' with Id: $managedIdentityId"
}
# Add Managed Identity to sqlusersgroup
$isInGroup = (az ad group member check --group $sqlUsersGroupId --member-id $managedIdentityId  --query value --output tsv)
if ($isInGroup -eq 'false') {
    az ad group member add --group $sqlUsersGroupId --member-id $managedIdentityId
    Write-Host "##[section]Added Entra Managed Identity '$appServiceWebName' with id '$managedIdentityId' to group: $sqlUsersGroupId"
}
Write-Host "##[warning]--- Create Managed Identity for Web API - END ---"

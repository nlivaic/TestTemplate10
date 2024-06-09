param ($keyVaultName)

# Will expose method NewPassword
. $PSScriptRoot\secretGenerator.ps1

################################################################
### Set Key Vault secrets to provided values.
################################################################
$secretName = "secretFromKeyVault111"

# We have to check whether all the relevant secrets are in there.
# If not, generate those secrets and store in Key Vault.
$query = "contains([].id, 'https://$($keyVaultName).vault.azure.net/secrets/$($secretName)')"
$exists = az keyvault secret list --vault-name $keyVaultName --query $query
if ($exists -eq "false") {
	az keyvault secret set --vault-name $keyVaultName --name $secretName --value $(NewPassword)
	Write-Host "##[section]Set secret $secretName"
}

$sqlSaAdminPassword = NewPassword
$sqlEntraAdminPassword = NewPassword

Write-Host "sqlSaAdminPassword:: $sqlSaAdminPassword"
Write-Host "sqlEntraAdminPassword:: $sqlEntraAdminPassword"
Write-Host "##vso[task.setvariable variable=sqlSaAdminPassword;isoutput=true]$sqlSaAdminPassword"
Write-Host "##vso[task.setvariable variable=sqlEntraAdminPassword;isoutput=true]$sqlEntraAdminPassword"
param ($environment, $projectName, $resourceGroupName)

Write-Host "##[warning]------ Create Key Vault Resource START ------"
$tenantId = (az account list --query "[?isDefault].tenantId | [0]" --output tsv)
$jsonResultAll = az deployment group create --resource-group $resourceGroupName --template-file ./deployment/iacKeyVault.bicep --parameters environment=$environment projectName=$projectName tenantId=$tenantId | ConvertFrom-Json
$keyVaultName = $jsonResultAll.properties.outputs.$keyVaultName.value
Write-Host "##vso[task.setvariable variable=keyVaultName;isoutput=true]$keyVaultName"
Write-Host "##[warning]------ Create Key Vault Resource END ------"

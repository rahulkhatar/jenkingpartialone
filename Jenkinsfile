node {

    stage('Checkout') {
        checkout scm
    }

    stage('SonarQube Analysis') {
        // Replace 'MySonarQubeServer' with your Jenkins SonarQube server name
        withSonarQubeEnv('') {
            bat '''
                dotnet sonarscanner begin ^
                  /k:"JenkinsPartialOne" ^
                  /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml"
            '''

            bat 'dotnet build'

            bat '''
                dotnet test ^
                  /p:CollectCoverage=true ^
                  /p:CoverletOutputFormat=opencover ^
                  /p:CoverletOutput=TestResults/coverage.opencover.xml
            '''

            bat 'dotnet sonarscanner end'
        }
    }

    stage('Publish') {
        bat '''
            echo Publishing .NET app...
            dotnet publish -c Release -o publish /p:DeployOnBuild=true /p:WebPublishMethod=Package

            echo Zipping publish folder for Azure Web App...
            powershell -Command "Remove-Item publish\\app.zip -ErrorAction SilentlyContinue"
            powershell -Command "cd publish; Compress-Archive -Path * -DestinationPath ..\\publish\\app.zip -Force"

            echo Published files:
            dir publish
        '''
    }

    stage('Deploy to Azure Web App') {
        withCredentials([
            string(credentialsId: 'azure-client-id', variable: 'AZ_CLIENT_ID'),
            string(credentialsId: 'azure-client-secret', variable: 'AZ_CLIENT_SECRET'),
            string(credentialsId: 'azure-tenant-id', variable: 'AZ_TENANT_ID'),
            string(credentialsId: 'azure-subscription-id', variable: 'AZ_SUBSCRIPTION_ID')
        ]) {
            bat "echo Logging into Azure..."
bat """
az login --service-principal ^
  --username %AZ_CLIENT_ID% ^
  --password %AZ_CLIENT_SECRET% ^
  --tenant %AZ_TENANT_ID%
"""
bat "az account set --subscription %AZ_SUBSCRIPTION_ID%"
bat """
set ZIP_PATH=%WORKSPACE%\\publish\\app.zip
echo Deploying using zip: %ZIP_PATH%
az webapp deploy ^
  --resource-group rg-restaurants-dev ^
  --name restaurant-api-dev ^
  --src-path %ZIP_PATH% ^
  --type zip
"""
bat "echo Deployment complete. Verify your API URL."
        }
    }

}

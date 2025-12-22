node {
  stage('Checkout') {
    checkout scm
  }

  stage('SonarQube Analysis') {
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
      dotnet publish -c Release -o publish /p:PublishReadyToRun=true /p:UseAppHost=true
      powershell -Command "Remove-Item publish\\app.zip -ErrorAction SilentlyContinue"
      powershell -Command "cd publish; Compress-Archive -Path * -DestinationPath ..\\publish\\app.zip -Force"
    '''
}

  stage('Deploy to Azure Web App') {
    withCredentials([
      string(credentialsId: 'azure-client-id', variable: 'AZ_CLIENT_ID'),
      string(credentialsId: 'azure-client-secret', variable: 'AZ_CLIENT_SECRET'),
      string(credentialsId: 'azure-tenant-id', variable: 'AZ_TENANT_ID'),
      string(credentialsId: 'azure-subscription-id', variable: 'AZ_SUBSCRIPTION_ID')
    ]) {
      bat '''
        echo Logging into Azure...
        az login --service-principal ^
          --username %AZ_CLIENT_ID% ^
          --password %AZ_CLIENT_SECRET% ^
          --tenant %AZ_TENANT_ID%

        az account set --subscription %AZ_SUBSCRIPTION_ID%

        echo Deploying to Azure Web App...
        az webapp deploy ^
          --resource-group rg-restaurants-dev ^
          --name restaurant-api-dev ^
          --src-path publish\\app.zip ^
          --type zip
      '''
    }
  }


}

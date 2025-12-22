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

  stage('Deploy to Azure Web App') {
  withCredentials([string(credentialsId: 'azure-sp-restaurant-dev', variable: 'AZURE_CREDENTIALS')]) {
    bat '''
      echo %AZURE_CREDENTIALS% > azureauth.json
      az login --service-principal --username $(jq -r '.clientId' azureauth.json) --password $(jq -r '.clientSecret' azureauth.json) --tenant $(jq -r '.tenantId' azureauth.json)
      az webapp deploy --resource-group myResourceGroup --name restaurant-dev --src-path path\\to\\your\\package.zip
    '''
  }
}
}

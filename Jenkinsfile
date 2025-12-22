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
      dotnet publish -c Release -o publish
      powershell Compress-Archive -Path publish\\* -DestinationPath publish\\app.zip -Force
    '''
  }

  stage('Deploy to Azure Web App') {
  azureWebAppPublish(
    azureCredentialsId: 'azure-sp-restaurant-dev',
    resourceGroup: 're-restaurant-dev',
    appName: 'restaurant-api-dev',
    sourceDirectory: 'publish'
  )
}

}

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
  withCredentials([
    string(credentialsId: '5b96e347-9411-441b-8de1-519b40c3546f'),
    string(credentialsId: 'Ztz8Q~f4a7IvFo3euo2sQL8CI1.HC2PYSXoeUaZq'),
    string(credentialsId: '4250280e-1ebf-49e4-8d2d-03a2d5fdb38b'),
    string(credentialsId: '7704f293-e06d-4c3e-9c65-423d08206b48')
  ]) {
    bat '''
      az login --service-principal ^
        --username %AZ_CLIENT_ID% ^
        --password %AZ_CLIENT_SECRET% ^
        --tenant %AZ_TENANT_ID%

      az account set --subscription %AZ_SUBSCRIPTION_ID%

      az webapp deploy ^
        --resource-group rg-restaurant-dev ^
        --name restaurant-api-dev ^
        --src-path publish\\app.zip ^
        --type zip
    '''
  }
}


}

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
}

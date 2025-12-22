node {
  stage('Checkout') {
    checkout scm
  }
  stage('SonarQube Analysis') {
    withSonarQubeEnv() {
      bat '''
        dotnet sonarscanner begin ^
        /k:"JenkinsPartialOne" ^
        /d:sonar.cs.opencover.reportsPaths="**/coverage.cobertura.xml"
      '''
      bat 'dotnet build'
      bat 'dotnet test --collect:"XPlat Code Coverage"'
      bat 'dotnet sonarscanner end'
    }
  }
}

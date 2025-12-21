node {
  stage('SCM') {
    checkout scm
  }
  stage('SonarQube Analysis') {
    # def scannerHome = tool 'SonarScanner for .NET'
    withSonarQubeEnv() {
      # bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll begin /k:\"JenkinsPartialOne\""
      bat "dotnet sonarscanner begin /k:"JenkinsPartialOne\""
      bat "dotnet build"
      bat "dotnet sonarscanner end"
    }
  }
}

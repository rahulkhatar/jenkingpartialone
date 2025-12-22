node {
  stage('SCM') {
    checkout scm
  }
  stage('SonarQube Analysis') {
    withSonarQubeEnv() {
      bat 'dotnet sonarscanner begin /k:"JenkinsPartialOne"'
      bat "dotnet build"
      bat "dotnet sonarscanner end"
    }
  }
}

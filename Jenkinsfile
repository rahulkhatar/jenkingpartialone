node {
  stage('SCM') {
    checkout scm
  }
  stage('SonarQube Analysis') {
    withSonarQubeEnv() {
      bat 'dotnet sonarscanner begin /k:"JenkinsPartialOne" /d:sonar.host.url="http://localhost:9000/" /d:sonar.login="sqa_1da3774d86d65fb22b1c4d6a843b3663961e1535"'
      bat "dotnet build"
      bat "dotnet sonarscanner end"
    }
  }
}

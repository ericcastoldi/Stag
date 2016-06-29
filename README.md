# Stag
Assistente de interação com Git e GitLab.

[![Build status](https://ci.appveyor.com/api/projects/status/github/ericcastoldi/stag?svg=true)](https://ci.appveyor.com/project/ericcastoldi/stag)

## Configuração
Configurar as informações de usuario e workspace no arquivo `config.json`:

```
{
  "Username" : "Eric.Castoldi", // Nome de usuário no git/gitlab
  "Email" : "eric.castoldi@senior.com.br", // Email 
  "Password" : "******", // Senha de rede
  "RemoteUrl" : "http://xpto.com/repo.git", // Url do remote no gitlab
  "Workspace" : "C:\\git\\workspace\\", // Diretório raiz da solution e do repositório
  "WorkBranch" : "master", // Branch de trabalho (mercado, os ou projeto)
  // Daqui pra baixo não é mais utilizado, pode ser informado qualquer coisa:
  "TaskBranchPrefix" : "feature",
  "MergeBranchPrefix" : "merge",
  "GsbdBranchPrefix" : "gsbd",
  "GitLabPrivateToken" : "xmnbsdiufhasdkjfnaoisudfh",
  "GitLabProjectName" : "sde-fontes",
  "GitLabApproverUsername" : "Eric.Castoldi",
  "StorageBasePath" : "C:\\Stag",
  "MiscTaskId": "342365" 
}
```

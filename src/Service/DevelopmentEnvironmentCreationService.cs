using LibGit2Sharp;
using Stag.Configuration;
using Stag.SourceControl;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Stag.Service
{
    public delegate void DevelopmentEnvironmentCreationMessageEmited(string message);

    public delegate void DevelopmentEnvironmentCreationDone(ServiceResult result);

    public class DevelopmentEnvironmentCreationService
    {
        private readonly string _version;
        private readonly string _branch;
        private readonly string _taskBranchName;
        private readonly ISettings _settings;
        private readonly Git _git;

        internal DevelopmentEnvironmentCreationService(ISettings settings, string version, string branch, string taskBranchName)
        {
            _branch = branch;
            _version = version;
            _taskBranchName = taskBranchName;
            _settings = settings;
            _git = new Git(_settings);
        }

        public event DevelopmentEnvironmentCreationDone Done;

        public event DevelopmentEnvironmentCreationMessageEmited MessageEmited;

        public void CreateEnvironment()
        {
            /*
             * Checkout branch OS
             * PULL
             * Checkout master
             * PULL
             * Altera Versao
             * Compila solution
             * Chama "Configurador.exe gsbd"

             * Cria branch tarefa
             * Altera connection strings do web.config e app.config
             * Abre VS
             */

            try
            {
                if (string.IsNullOrWhiteSpace(_version) || string.IsNullOrWhiteSpace(_branch) || string.IsNullOrWhiteSpace(_taskBranchName))
                {
                    throw new InvalidOperationException("Informe a versão, o branch de origem e o nome do branch de feature a ser criado.");
                }

                var currentBranch = _git.GetCurrentBranch();
                EmitMessage(string.Format("Iniciando processo de criação do ambiente de desenvolvimento. Descartando todas as alterações existentes no branch {0}, workspace {1}...", currentBranch.Name, _settings.Workspace));
                _git.HardReset();

                EmitMessage(string.Format("Fazendo checkout do branch {0}...", _branch));
                _git.Checkout(_branch);

                EmitMessage(string.Format("Atualizando branch {0}...", _branch));
                _git.UpdateCurrentBranch();

                EmitMessage(string.Format("Fazendo checkout do branch {0}...", "master"));
                _git.Checkout("master");

                EmitMessage(string.Format("Atualizando branch {0}...", "master"));
                _git.UpdateCurrentBranch();

                this.NugetRestore(_settings.Workspace);

                this.ChangeAssemblyInfoVersion(_settings.Workspace);

                this.CompileSolution(_settings.Workspace);

                this.InitializeDatabaseConfiguration(_settings.Workspace);

                EmitMessage(string.Format("Criando o branch {0} a partir do branch {1}...", _taskBranchName, _branch));
                _git.CreateBranch(_taskBranchName, _branch);

                try
                {
                    EmitMessage(string.Format("Fazendo checkout do branch {0}...", _taskBranchName));
                    _git.Checkout(_taskBranchName);
                }
                catch (MergeConflictException ex)
                {
                    EmitMessage(string.Format("Ocorreu um erro ao fazer o checkout do branch {0}. Erro: {1}. Fazendo reset das alterações do branch...", _taskBranchName, ex.Message));
                    _git.HardReset();

                    EmitMessage(string.Format("Tentando fazer o checkout do branch {0} novamente...", _taskBranchName));
                    _git.Checkout(_taskBranchName);

                    EmitMessage(string.Format("Em razão do erro no checkout do branch {0} será necessário configurar a base novamente para que os arquivos *.config do repositório {1} tenham suas connection strings atualizadas. ", _taskBranchName, _settings.Workspace));

                    this.InitializeDatabaseConfiguration(_settings.Workspace);
                }

                var solutionPath = _settings.Workspace + "\\SapiensNfe.sln";
                EmitMessage(string.Format("Abrindo Visual Studio. Solution: '{0}'.", solutionPath));
                Process.Start("C:\\Program Files (x86)\\Microsoft Visual Studio 11.0\\Common7\\IDE\\devenv.exe", solutionPath);

                var msg = "Criação do ambiente de desenvolvimento completada com sucesso!";
                EmitMessage(msg);

                this.CreationCompleted(msg, true);
            }
            catch (Exception ex)
            {
                this.CreationCompleted(ex.Message, false);
            }
        }

        private void CreationCompleted(string message, bool success)
        {
            if (this.Done != null)
            {
                var info = new ServiceResult(success, message);
                this.Done(info);
            }
        }

        private void NugetRestore(string workspace)
        {
            EmitMessage("Executando o nuget restore (NuGet.exe RESTORE SapiensNfe.sln)...");
            this.RunProcess(workspace, string.Format("{0}\\.nuget\\NuGet.exe", workspace), "RESTORE SapiensNfe.sln");
        }

        private void ChangeAssemblyInfoVersion(string workspace)
        {
            EmitMessage(string.Format("Alterando a versão dos AssemblyInfo.cs da solution '{0}\\SapiensNfe.sln' para '{1}'...", workspace, _version));

            //"C:\\github\\Stag\\src\\bin\\Debug\\Resources\AlteraVersao.bat"
            var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Resources");
            this.RunProcess(path, path + "\\AlteraVersao.bat", string.Format("\"{0}\" {1}", workspace, _version));
        }

        private void CompileSolution(string workspace)
        {
            EmitMessage(string.Format("Compilando a solution '{0}\\SapiensNfe.sln'...", workspace));
            this.RunProcess(workspace, "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe", "/p:Configuration=Debug /v:normal /t:Clean;Build /m SapiensNfe.sln");
        }

        private void InitializeDatabaseConfiguration(string workspace)
        {
            EmitMessage("Inicializando o configurador de banco de dados...");

            var configurador = string.Format("{0}\\Configurador\\bin\\Debug\\Senior.SapiensNfe.Configurador.exe", workspace);
            this.RunProcess(workspace, configurador, "gsbd");
        }

        private void RunProcess(string workspace, string command, string args)
        {
            var processInfo = new ProcessStartInfo(command, args);
            processInfo.WorkingDirectory = workspace;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.CreateNoWindow = true;
            processInfo.StandardErrorEncoding = Encoding.ASCII;
            processInfo.StandardOutputEncoding = Encoding.ASCII;

            var proc = Process.Start(processInfo);
            var stdout = proc.StandardOutput;
            var stderr = proc.StandardError;

            this.EmitMessagesFromStream(stdout);
            this.EmitMessagesFromStream(stderr);
        }

        private void EmitMessagesFromStream(StreamReader streamReader)
        {
            string line;

            while ((line = streamReader.ReadLine()) != null)
            {
                EmitMessage(line);
            }
        }

        private void EmitMessage(string message)
        {
            if (this.MessageEmited != null)
            {
                this.MessageEmited(message);
            }
        }
    }
}
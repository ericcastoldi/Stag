using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.Abstractions;
using System.Reflection;

namespace Stag.Service
{
    public class XsdWrapperService
    {
        private const string XsdFileExtension = "*.xsd";
        private const string XsdExeFileName = "xsd.exe";
        private const string OutputDirectoryName = "_generatedClasses";
        private const string DigitalSignatureSchema = "xmldsig-core-schema_v1.01.xsd";
        private IProcessRunner _runner;
        private readonly string _binPath;
        private readonly IFileSystem _fileSystem;

        public XsdWrapperService()
            : this(new FileSystem(), new ProcessRunner())
        {
        }

        public XsdWrapperService(IFileSystem fileSystem, IProcessRunner runner)
        {
            _runner = runner;
            _fileSystem = fileSystem;

            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            _binPath = _fileSystem.Path.GetDirectoryName(assemblyName.CodeBase);
        }

        public IEnumerable<string> GenerateClasses(string schemasDirectory, string classesNamespace, bool retry)
        {
            CreateOutputDirectory(schemasDirectory);

            var processInfo = ConfigureProcess(schemasDirectory);
            var dir = _fileSystem.Directory.EnumerateFiles(schemasDirectory, XsdFileExtension);

            foreach (var fileName in dir)
            {
                //var dsigSchema = _fileSystem.Path.Combine(schemasDirectory, DigitalSignatureSchema);
                //processInfo.Arguments = string.Format("\"{0}\" \"{1}\" /c /order /n:{2} /nologo /o:{3}", fileName, dsigSchema, classesNamespace, OutputDirectoryName);
                processInfo.Arguments = string.Format("\"{0}\" /c /order /n:{1} /nologo /o:{2}", fileName, classesNamespace, OutputDirectoryName);
                var process = _runner.Start(processInfo);

                while (!process.StandardError.EndOfStream)
                {
                    var errorMessage = process.StandardError.ReadToEnd();
                    if (retry && (errorMessage.Contains("Elemento 'http://www.w3.org/2000/09/xmldsig#:Signature' ausente.")
                                || errorMessage.Contains("Tipo de dados 'http://www.w3.org/2000/09/xmldsig#:DigestValueType' ausente")))
                    {
                        errorMessage = RetryGeneration(fileName, schemasDirectory, classesNamespace);
                    }

                    if (!string.IsNullOrWhiteSpace(errorMessage))
                        yield return errorMessage;
                }
            }
        }

        private string RetryGeneration(string fileName, string schemasDirectory, string classesNamespace)
        {
            var processInfo = ConfigureProcess(schemasDirectory);

            var dsigSchema = _fileSystem.Path.Combine(schemasDirectory, DigitalSignatureSchema);
            processInfo.Arguments = string.Format(CultureInfo.InvariantCulture, "\"{0}\" \"{1}\" /c /order /n:{2} /nologo /o:{3}", dsigSchema, fileName, classesNamespace, OutputDirectoryName);
            var process = _runner.Start(processInfo);

            var retryMessages = new List<String>();
            while (!process.StandardError.EndOfStream)
                retryMessages.Add(process.StandardError.ReadLine());

            return string.Join("\r\n", retryMessages);
        }

        private void CreateOutputDirectory(string schemasDirectory)
        {
            var generatedClassesOutput = _fileSystem.Path.Combine(schemasDirectory, OutputDirectoryName);
            if (!_fileSystem.Directory.Exists(generatedClassesOutput))
                _fileSystem.Directory.CreateDirectory(generatedClassesOutput);
        }

        private ProcessStartInfo ConfigureProcess(string schemasDirectory)
        {
            var processInfo = new ProcessStartInfo();

            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.CreateNoWindow = true;
            processInfo.ErrorDialog = true;
            processInfo.FileName = _fileSystem.Path.Combine(_binPath, "Resources", XsdExeFileName).Replace("file:\\", string.Empty);
            processInfo.WorkingDirectory = schemasDirectory;

            return processInfo;
        }
    }

    public interface IProcessRunner
    {
        System.Diagnostics.Process Start(System.Diagnostics.ProcessStartInfo startInfo);
    }

    public class ProcessRunner : IProcessRunner
    {
        public System.Diagnostics.Process Start(System.Diagnostics.ProcessStartInfo startInfo)
        {
            return System.Diagnostics.Process.Start(startInfo);
        }
    }
}
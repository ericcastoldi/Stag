@echo off

echo Iniciando alteracao da versao dos fontes...

echo.
if "%1" == "" (
	echo Informe o diretorio da solution que tera sua versao alterada:
	SET /P diretorio=
) else (
	echo Serao alteradas as versoes dos AssemblyInfo.cs e do comum.nsi do diretorio %~1
	set diretorio=%~1
)
echo.

echo.
if "%2" == "" (
	echo Informe a versao a ser aplicada nos fontes do diretorio %diretorio%:
	SET /P versao=
) else (
	echo Alterando fontes do diretorio %diretorio% para a versao %2
	SET versao=%2
)
echo.

SET pasta_ferramentas=%diretorio%packages\Senior.1.0.6\tools\
echo Caminho do alterador de versoes: %pasta_ferramentas%SubstituiVersao.exe 

echo Alterando versao do %diretorio%Configurador\Properties\AssemblyInfo.cs para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%Configurador\Properties\AssemblyInfo.cs %versao%

echo Alterando versao do %diretorio%DataAccess\Properties\AssemblyInfo.cs para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%DataAccess\Properties\AssemblyInfo.cs %versao%

echo Alterando versao do %diretorio%Importador\Properties\AssemblyInfo.cs para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%Importador\Properties\AssemblyInfo.cs %versao% 

echo Alterando versao do %diretorio%Monitor\Properties\AssemblyInfo.cs para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%Monitor\Properties\AssemblyInfo.cs %versao%

echo Alterando versao do %diretorio%PrintService\Properties\AssemblyInfo.cs para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%PrintService\Properties\AssemblyInfo.cs %versao%

echo Alterando versao do %diretorio%Processos\Properties\AssemblyInfo.cs para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%Processos\Properties\AssemblyInfo.cs %versao%

echo Alterando versao do %diretorio%Sde\Properties\AssemblyInfo.cs para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%Sde\Properties\AssemblyInfo.cs %versao%

echo Alterando versao do %diretorio%Instalador\comum.nsi para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%Instalador\comum.nsi %versao%

echo Alterando versao do %diretorio%SdeCliente\Properties\AssemblyInfo.cs para %versao%
%pasta_ferramentas%SubstituiVersao.exe %diretorio%SdeCliente\Properties\AssemblyInfo.cs %versao%


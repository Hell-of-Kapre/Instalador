using System;
using System.IO;
using System.Net;
using System.IO.Compression;

namespace InstaladorHOK
{
    internal class Program
    {
        public string dirgame = "C:/Program Files (x86)/Steam/steamapps/common/valheim";
        public string dirselected = null;
        static Program programa = new Program();
        
        //
        enum opcao
        {
            ValheimPlus = 1,
            Desinstalar,
            Atualizar,
        };

        public static void JoinServer()
        {
            System.Diagnostics.Process.Start("steam://connect/play.gkhosting.net:2457");
        }

        public static void Menu()
        {
            Console.WriteLine("========== HOK ==========\n");
            Console.WriteLine("ANTES DE TUDO, FECHE O SEU JOGO!");
            Console.WriteLine("Bem vindo ao instalador da Hell of Krape");
            Console.WriteLine("Siga os passos e as instruções quando o instalador pedir, não \'selecione\' nada na janela.");
            Console.WriteLine("Aguarde enquanto o instalador faz o trabalho ;D\n");
        }

        public static void ChoseOption()
        {
            int svopsecec;
            Console.WriteLine("========== Inicializador HOK ==========\n");
            Console.WriteLine("Olá jogador, seja bem vindo ao inicializador da HOK.");
            Console.WriteLine("O que você quer fazer hoje?");
            //Escolhendo Entrada
            Console.WriteLine("Deseja entrar no servidor?");
            Console.WriteLine("[1] Entrar no Servidor");
            Console.WriteLine("[2] Instalar/Atualizar/Desinstalar");
            Console.Write("> ");
            svopsecec = int.Parse(Console.ReadLine());
            //Entrada
            if (svopsecec == 1)
                JoinServer();
            else if (svopsecec == 2)
            {
                Console.Clear();
                return;
            }
            else
            {
                Console.WriteLine("\nOpção Inválida, reiniciando...");
                Console.Clear();
                ChoseOption();
            }
        }

        public static void InstallMenu()
        {
            Menu();
            Console.WriteLine("O que você quer fazer?");
            Console.WriteLine("[1] Instalar Mods");
            Console.WriteLine("[2] Desinstalar Mods");
            Console.WriteLine("[3] Atualizar Mods");
            Console.Write("> ");
        }

        public bool TestDefaultDirectory()
        {
            Menu();
            Console.WriteLine("Teste do diretório padrão...");
            bool exists = System.IO.Directory.Exists(dirgame);

            if (exists)
            {
                Console.WriteLine("Diretório padrão encontrado! Procurando por Valheim.exe");
                Directory.SetCurrentDirectory(dirgame);
                System.Threading.Thread.Sleep(2000);
                SearchingValheimExe(dirgame);
                Console.WriteLine("Valheim.exe foi encontrado, continuando...");
                SearchingBepInExInstall(dirgame);
                System.Threading.Thread.Sleep(3000);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Diretório padrão não encontrado, continuando...");
                System.Threading.Thread.Sleep(8000);
                Console.Clear();
            }

            return exists;
        }

        public static void SearchingDirectory(string dir)
        {
            bool exists = System.IO.Directory.Exists(dir);
            try
            {
                Directory.SetCurrentDirectory(dir);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("\nDiretório não encontrado: " + $"'{dir}'");
                System.Threading.Thread.Sleep(2500);
                Console.Clear();
                programa.SelectDirectory();
            }

            if (exists)
            {
                Console.WriteLine("\nDiretório encontrado, procurando valheim.exe");
                SearchingValheimExe(dir);
                System.Threading.Thread.Sleep(2000);
            }
        }

        public static void SearchingValheimExe(string dir)
        {
            bool existsexe = System.IO.File.Exists($"{dir}/valheim.exe");
            if (existsexe)
            {
                Console.WriteLine("Valheim.Exe foi encontrado, continuando a instalação...");
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Valheim.Exe não foi encontrado, reiniciando...");
                System.Threading.Thread.Sleep(4000);
                Console.Clear();
                programa.SelectDirectory();
            }
        }

        public static void SearchingBepInExInstall(string dir)
        {
            //Console.WriteLine("SearchingBepInEx Dir Recebido: " + dir);
            bool existsbepinex = false;
            bool existsdoorstop = System.IO.File.Exists($"{dir}/doorstop_config.ini");
            bool existswinhttp = System.IO.File.Exists($"{dir}/winhttp.dll");
            bool existsbepfolder = System.IO.Directory.Exists($"{dir}/BepInEx");
            bool existsdorstoplibs = System.IO.Directory.Exists($"{dir}/doorstop_libs");
            bool existsunstripped = System.IO.Directory.Exists($"{dir}/unstripped_corlib");
            
            if (existsdoorstop || existsbepfolder || existsdorstoplibs || existswinhttp || existsunstripped)
            {
                Console.WriteLine("existsunstripped" + existsunstripped);
                Console.WriteLine("existswinhttp" + existswinhttp);
                Console.WriteLine("existsbepfolder" + existsbepfolder);
                Console.WriteLine("existsdorstoplibs" + existsdorstoplibs);
                Console.WriteLine("existsunstripped" + existsunstripped);
                existsbepinex = true;
            }

            if (existsbepinex)
            {
                Console.WriteLine("\nOs seguintes arquivos/diretórios: BepInEx, doorstop_libs, unstripped_corlib, doorstop_config.ini ou winhttp.dll foram encontrados na pasta selecionada.");
                Console.WriteLine("Você quer remover esses arquivos? Tudo será deletado.");
                Console.WriteLine("[1] Sim");
                Console.WriteLine("[2] Não");
                Console.Write("> ");
                int opcselec = int.Parse(Console.ReadLine());

                if (opcselec == 1)
                {
                    Console.WriteLine("Removendo Arquivos...");
                    System.Threading.Thread.Sleep(2000);
                    
                    //Deleting Files
                    if(existsdoorstop)
                        File.Delete("doorstop_config.ini");
                    if(existswinhttp)
                        File.Delete("winhttp.dll");
                    if (existsbepfolder)
                    {
                        string bepinexdir = $"{Directory.GetCurrentDirectory()}/BepInEx";
                        DirectoryInfo directorybep = new DirectoryInfo(bepinexdir);
                        directorybep.Delete(true);
                    }
                    if (existsdorstoplibs)
                    {
                        string dorstopdir = $"{Directory.GetCurrentDirectory()}/doorstop_libs";
                        DirectoryInfo directorydorstop = new DirectoryInfo(dorstopdir);
                        directorydorstop.Delete(true);
                    }
                    if (existsunstripped)
                    {
                        string unstrippeddir = $"{Directory.GetCurrentDirectory()}/unstripped_corlib";
                        DirectoryInfo directoryunstrip = new DirectoryInfo(unstrippeddir);
                        directoryunstrip.Delete(true);
                    }
                    Console.WriteLine("\nRemoção de arquivo concluída com sucesso!");
                    System.Threading.Thread.Sleep(2000);
                }
                else if (opcselec == 2)
                {
                    Console.WriteLine("Ok, se você quiser instalar o mod você deve apagar arquivos de instalações anteriores ou selecionar a opção \"Atualizar Mods\".");
                    System.Threading.Thread.Sleep(8000);
                    InstallMenu();
                }
                else
                {
                    Console.WriteLine("Opção Inválida");
                    Console.WriteLine("[1] Sim");
                    Console.WriteLine("[2] Não");
                    Console.Write("> ");
                    opcselec = int.Parse(Console.ReadLine());;
                }
                
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Nenhuma instalação anterior foi encontrada, continuando...");
                System.Threading.Thread.Sleep(2000);
            }
        }

        public string SelectDirectory()
        {
            try
            {
                Menu();
                Console.WriteLine("Primeiro, escolha o diretório.");
                Console.WriteLine("Normalmente ele fica em: C:/Program Files (x86)/Steam/steamapps/common/valheim");
                Console.WriteLine("Mas o seu jogo pode estar em outro local, como por exemplo: E:/Games/Valheim");
                Console.WriteLine("Precisamos que você escreva o diretório completo, sem erros.");
                Console.WriteLine("Em qual diretório está seu jogo?\n");
                dirselected = Console.ReadLine();
                SearchingDirectory(dirselected);
                Console.Clear();
                return dirselected;
            }
            catch (Exception)
            {
                Console.WriteLine("Diretório Inválido, inexistente ou errado...");
                System.Threading.Thread.Sleep(2000);
                System.Environment.Exit(0);
                throw;
            }
        }

        public static void JoinOption(int svopsecec)
        {
            if (svopsecec == 1)
                JoinServer();
            else if (svopsecec == 2)
            {
                Console.WriteLine("\nTchau!");
                System.Threading.Thread.Sleep(5000);
            }
            else
            {
                Console.WriteLine("\nOpção Inválida, reiniciando...");
                JoinOption(svopsecec);
            }
        }

        public static void FinishThanks()
        {
            int svopsecec;
            Console.WriteLine("========== HOK ==========\n");
            Console.WriteLine("Seja bem vindo ao Hell of Kapre, os mods foram instalados com sucesso!");
            Console.WriteLine("O instalador fechará em 10 segundos...");
            Console.WriteLine("\nCriado Por: CastBlacKing\n");
            //Escolhendo Entrada
            Console.WriteLine("Deseja entrar no servidor?");
            Console.WriteLine("[1] Sim");
            Console.WriteLine("[2] Não");
            Console.Write("> ");
            svopsecec = int.Parse(Console.ReadLine());;
            //Entrada
            JoinOption(svopsecec);
        }
        
        public static void Goodbye()
        {
            Console.WriteLine("========== HOK ==========\n");
            Console.WriteLine("Os mods foram desinstalados com sucesso, adeus :(");
            Console.WriteLine("O instalador fechará em 10 segundos...");
            Console.WriteLine("\nCriado Por: CastBlacKing");
            System.Threading.Thread.Sleep(10000);
        }
        
        public static void UpdateMessage()
        {
            int svopsecec;
            Console.WriteLine("========== HOK ==========\n");
            Console.WriteLine("Prontinho, tudo foi atualizado com sucesso!!");
            Console.WriteLine("O instalador fechará em 10 segundos...");
            Console.WriteLine("\nCriado Por: CastBlacKing");
            System.Threading.Thread.Sleep(1000);
            //Escolhendo Entrada
            Console.WriteLine("Deseja entrar no servidor?");
            Console.WriteLine("[1] Sim");
            Console.WriteLine("[2] Não");
            Console.Write("> ");
            svopsecec = int.Parse(Console.ReadLine());;
            //Entrada
            JoinOption(svopsecec);
        }

        public static void InstallValheimPlus()
        {
            // Baixando o Core
            try
            {
                SearchingBepInExInstall(programa.dirselected);
                WebClient webClient = new WebClient();
                Console.WriteLine("\nIniciando o download....");
                webClient.DownloadFile("https://gkhosting.com.br/valheim/Release.zip", "Release.zip");
                Console.WriteLine("\nArquivo baixado com sucesso! Extraindo arquivo para o diretório do jogo...");
                ZipFile.ExtractToDirectory("Release.zip", $"{Directory.GetCurrentDirectory()}");
            }
            catch (Exception)
            {
                Console.WriteLine("O download e a extração do núcleo falharam...");
                Console.WriteLine("As vezes isto acontece por que o instalador não tem permissão para apagar os arquivos.");
                Console.WriteLine("Por favor, delete os seguintes arquivos/pastas que estão no diretório do seu jogo:");
                Console.WriteLine("/doorstop_config.ini");
                Console.WriteLine("/winhttp.dll");
                Console.WriteLine("/BepInEx");
                Console.WriteLine("/doorstop_libs");
                Console.WriteLine("/unstripped_corlib");
                Console.WriteLine("Depois de remover eles, por favor, aperte enter para o instalador fechar,\n depois disto abra o instalador e escolha a opção \'instalar\'");
                System.Environment.Exit(0);
            }
            
            
            // Limpeza de Desnecessários
            System.Threading.Thread.Sleep(2500);
            Console.WriteLine("Deletando arquivo baixado .zip");
            File.Delete("Release.zip");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public static void FullClean(string dir)
        {
            try
            {
                bool existsdoorstop = System.IO.File.Exists($"{dir}/doorstop_config.ini");
                bool existswinhttp = System.IO.File.Exists($"{dir}/winhttp.dll");
                bool existsbepfolder = System.IO.Directory.Exists($"{dir}/BepInEx");
                bool existsdorstoplibs = System.IO.Directory.Exists($"{dir}/doorstop_libs");
                bool existsunstripped = System.IO.Directory.Exists($"{dir}/unstripped_corlib");
                Console.WriteLine("Removendo Arquivos...");
                System.Threading.Thread.Sleep(2000);
                
                //Deleting Files
                if(existsdoorstop)
                    File.Delete("doorstop_config.ini");
                if(existswinhttp)
                    File.Delete("winhttp.dll");
                if (existsbepfolder)
                {
                    string bepinexdir = $"{Directory.GetCurrentDirectory()}/BepInEx";
                    DirectoryInfo directorybep = new DirectoryInfo(bepinexdir);
                    directorybep.Delete(true);
                }
                if (existsdorstoplibs)
                {
                    string dorstopdir = $"{Directory.GetCurrentDirectory()}/doorstop_libs";
                    DirectoryInfo directorydorstop = new DirectoryInfo(dorstopdir);
                    directorydorstop.Delete(true);
                }
                if (existsunstripped)
                {
                    string unstrippeddir = $"{Directory.GetCurrentDirectory()}/unstripped_corlib";
                    DirectoryInfo directoryunstrip = new DirectoryInfo(unstrippeddir);
                    directoryunstrip.Delete(true);
                }
                Console.WriteLine("\nRemoção de Arquivos Concluída!");
                System.Threading.Thread.Sleep(2000);
            }
            catch (System.Exception)
            {
                Console.WriteLine("A remoção de arquivos falhou, reiniciando o instalador...");
                System.Threading.Thread.Sleep(2000);
                InstallMenu();
            }
        }

        //Função principal
        public static void Main(string[] args)
        {
            ChoseOption();

            //Chamando o programa
            //Tetando o padrão
            if(programa.TestDefaultDirectory())
            {
            }
            else
            { 
                programa.SelectDirectory();
            }

            //Menu de instalação
            InstallMenu();
            int index = int.Parse(Console.ReadLine());
            opcao opcaoSelecionada = (opcao)index;
            
            //Selector
            switch (opcaoSelecionada)
            {
             case opcao.ValheimPlus:
                 InstallValheimPlus();
                 FinishThanks();
                 break;
             case opcao.Desinstalar:
                 FullClean(programa.dirselected);
                 Console.Clear();
                 Goodbye();
                 break;
             case opcao.Atualizar:
                 // Atualizando o Brasil Mod
                 FullClean(programa.dirselected);
                 InstallValheimPlus();
                 UpdateMessage();
                 break;
            }
        }


    }
}
﻿using SistemaEnsino.models;

Console.WriteLine("Bem-Vindo ao Menu da Instituição!");

var alunos = new List<Aluno>
{
    new("Pablo", "pablo@email.com", 20, "1234567890"),
    new("Pedro", "pedro@email.com", 19, "1234567891"),
    new("Paulo", "paulo@email.com", 21, "1234567892")
};

var cursos = new List<Curso>
{
    new("Curso de C#"),
    new("Curso de Java"),
    new("Curso de Python")
};

var professores = new List<Professor>
{
    new("Rosen", "rosen@email.com", 30),
    new("Venício", "venicio@hotmail.com", 200),
    new("Rafael", "rafael@outlook.com", 42)
};

cursos[0].Adicionar(alunos[0]);
cursos[0].Adicionar(alunos[1]);
cursos[0].Adicionar(professores[0]);

cursos[1].Adicionar(alunos[1]);
cursos[1].Adicionar(alunos[2]);
cursos[1].Adicionar(professores[1]);

cursos[2].Adicionar(alunos[2]);
cursos[2].Adicionar(alunos[0]);
cursos[2].Adicionar(professores[2]);

var continua = true;

do
{
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Menu de Aluno");
    Console.WriteLine("2 - Menu de Professor");
    Console.WriteLine("3 - Menu de Curso");
    Console.WriteLine("9 - Parar");
    
    var opcao = Console.ReadLine();
    
    switch (opcao)
    {
        case "1":
            Console.WriteLine("\nVocê escolheu o Menu de Aluno:");

            var continuaAluno = true;
            
            do
            {
                alunos.ForEach(aluno => Console.WriteLine($"{aluno.Nome}    Id: {aluno.Id}"));
            
                Console.WriteLine("\n1 - Adicionar Aluno");
                Console.WriteLine("2 - Atualizar Aluno");
                Console.WriteLine("3 - Remover Aluno");
                Console.WriteLine("4 - Exibir detalhe de Aluno");
                Console.WriteLine("9 - Voltar");
                
                var opcaoAluno = Console.ReadLine();
                
                switch (opcaoAluno)
                {
                    case "1":
                        Console.WriteLine("Adicionar Aluno");
                        
                        Console.WriteLine("Digite o nome:");
                        var nome = Console.ReadLine();
                        Console.WriteLine("Digite o email:");
                        var email = Console.ReadLine();
                        Console.WriteLine("Digite a idade:");
                        var idade = int.Parse(Console.ReadLine() ?? string.Empty);
                        Console.WriteLine("Digite a matrícula:");
                        var matricula = Console.ReadLine();
                        
                        alunos.Add(new Aluno(nome, email, idade, matricula));
                        Console.WriteLine("Aluno cadastrado com sucesso!\n");
                        break;
                    case "2":
                        Console.WriteLine("Atualizar Aluno");
                        
                        Console.WriteLine("Digite o ID do aluno que deseja atualizar:");
                        var id = int.Parse(Console.ReadLine() ?? string.Empty);
                        
                        var aluno = alunos.Find(a => a.Id.Equals(id));
                        
                        if (aluno != null)
                        {
                            Console.WriteLine("Digite o novo nome:");
                            var novoNome = Console.ReadLine();
                            aluno.Atualizar(novoNome);
                            Console.WriteLine("Aluno atualizado com sucesso!\n");
                        }
                        else
                        {
                            Console.WriteLine("Aluno não encontrado.\n");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Remover Aluno");
                        
                        Console.WriteLine("Digite o ID do aluno que deseja remover:");
                        var idRemove = int.Parse(Console.ReadLine() ?? string.Empty);
                        
                        var alunoRemove = alunos.Find(a => a.Id.Equals(idRemove));
                        
                        if (alunoRemove != null)
                        {
                            alunos.Remove(alunoRemove);
                            cursos.ForEach(curso => curso.Remover(alunoRemove.Id));
                            Console.WriteLine("Aluno removido com sucesso!\n");
                        }
                        else
                        {
                            Console.WriteLine("Aluno não encontrado.\n");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Exibir detalhe de Aluno");
                        
                        Console.WriteLine("Digite o ID do aluno que deseja saber mais:");
                        
                        var idDetalhe = int.Parse(Console.ReadLine() ?? string.Empty);
                        
                        var alunoDetalhe = alunos.Find(a => a.Id.Equals(idDetalhe));
                        
                        if (alunoDetalhe != null)
                        {
                            Console.WriteLine($"\nNome: {alunoDetalhe.Nome}");
                            Console.WriteLine($"Email: {alunoDetalhe.Email}");
                            Console.WriteLine($"Idade: {alunoDetalhe.Idade}");
                            Console.WriteLine($"Matrícula: {alunoDetalhe.Matricula}");
                            var listaDetalhe = alunoDetalhe.Listar();
                            Console.WriteLine($"Cursos: {(listaDetalhe != string.Empty ?  "\n" + listaDetalhe 
                                : "Aluno está sem cursos!\n")}");

                            Console.WriteLine("1 - Inscrever aluno em algum curso");
                            Console.WriteLine("2 - Remover aluno do curso");
                            Console.WriteLine("? - Voltar");
                            
                            var opcaoAlunoCurso = Console.ReadLine();
                            
                            switch (opcaoAlunoCurso)
                            {
                                case "1":
                                    cursos.ForEach(curso => Console.WriteLine($"{curso.Nome}    Id: {curso.Id}"));

                                    Console.WriteLine("\nDigite o ID do curso que deseja inscrever o aluno:");
                                    var idCurso = int.Parse(Console.ReadLine() ?? string.Empty);

                                    var curso = cursos.Find(curso => curso.Id.Equals(idCurso));

                                    if (curso != null)
                                    {
                                        var adicionado = alunoDetalhe.Adicionar(curso);
                                        Console.WriteLine($"{(adicionado ? "Aluno inscrito no curso\n" 
                                            : "Aluno já está fazendo o curso\n")}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Curso não encontrado!\n");
                                    }
                                    break;
                                case "2":
                                    alunoDetalhe.Cursos.ForEach(c => Console.WriteLine($"{c.Nome}    Id: {c.Id}"));

                                    Console.WriteLine("Digite o ID do curso que deseja inscrever o aluno:");
                                    var idCursoAluno = int.Parse(Console.ReadLine() ?? string.Empty);
                                    
                                    var removido = alunoDetalhe.Remover(idCursoAluno);

                                    Console.WriteLine($"{(removido ? "Aluno removido do curso\n" 
                                        : "Não foi possível remover aluno do curso\n")}");
                                    break;
                                default:
                                    Console.WriteLine("Voltando...\n");
                                    break;
                            } 
                        }
                        else
                        {
                            Console.WriteLine("Aluno não encontrado.\n");
                        }
                        break;
                    case "9":
                        continuaAluno = false;
                        Console.WriteLine("Voltando...\n");
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.\n");
                        break;
                }
            } while (continuaAluno);
            
            break;
        case "2":
            Console.WriteLine("\nProfessores:");

            var continuaProfessor = true;

            do
            {
                professores.ForEach(professor => Console.WriteLine($"{professor.Nome}    Id: {professor.Id}"));
                
                Console.WriteLine("\n1 - Adicionar Professor");
                Console.WriteLine("2 - Remover Professor");
                Console.WriteLine("9 - Voltar");
                
                var opcaoProfessor = Console.ReadLine();
                
                switch (opcaoProfessor)
                {
                    case "1":
                        Console.WriteLine("Vamos cadastrar um novo professor! Digite o nome:");
                        var nome = Console.ReadLine();
                        Console.WriteLine("Email:");
                        var email = Console.ReadLine();
                        Console.WriteLine("Idade:");
                        var idade = int.Parse(Console.ReadLine() ?? string.Empty);
                        
                        professores.Add(new Professor(nome, email, idade));
                        Console.WriteLine("Professor cadastrado com sucesso!\n");
                        break;
                    case "2":
                        Console.WriteLine("Remover Professor");
                        
                        Console.WriteLine("Digite o ID do professor que deseja remover:");
                        var id = int.Parse(Console.ReadLine() ?? string.Empty);
                        
                        var professor = professores.Find(p => p.Id.Equals(id));
                        
                        if (professor != null)
                        {
                            professores.Remove(professor);
                            Console.WriteLine("Professor removido com sucesso!\n");
                        }
                        else
                        {
                            Console.WriteLine("Professor não encontrado.\n");
                        }
                        break;
                    case "9":
                        continuaProfessor = false;
                        Console.WriteLine("Voltando...\n");
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.\n");
                        break;
                }
            } while (continuaProfessor);
            break;
        case "3":
            Console.WriteLine("\nNossos Cursos:");
            
            var continuaCurso = true;
            
            do
            {
                cursos.ForEach(curso => Console.WriteLine($"{curso.Nome}    Id: {curso.Id}"));
            
                Console.WriteLine("\n1 - Adicionar Curso");
                Console.WriteLine("2 - Atualizar Curso");
                Console.WriteLine("3 - Remover Curso");
                Console.WriteLine("4 - Exibir detalhes do Curso");
                Console.WriteLine("9 - Voltar");
                
                var opcaoCurso = Console.ReadLine();
                
                switch (opcaoCurso)
                {
                    case "1":
                        Console.WriteLine("Adicionar Curso");
                        
                        Console.WriteLine("Digite o nome:");
                        var nome = Console.ReadLine();
                        
                        cursos.Add(new Curso(nome));
                        Console.WriteLine("Curso cadastrado com sucesso!\n");
                        break;
                    case "2":
                        Console.WriteLine("Atualizar Curso");
                        
                        Console.WriteLine("Digite o ID do curso que deseja atualizar:");
                        var id = int.Parse(Console.ReadLine() ?? string.Empty);
                        
                        var curso = cursos.Find(c => c.Id.Equals(id));
                        
                        if (curso != null)
                        {
                            Console.WriteLine("Digite o novo nome:");
                            var novoNome = Console.ReadLine();
                            curso.Atualizar(novoNome);
                            Console.WriteLine("Curso atualizado com sucesso!\n");
                        }
                        else
                        {
                            Console.WriteLine("Curso não encontrado.\n");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Remover Curso");
                        
                        Console.WriteLine("Digite o ID do curso que deseja remover:");
                        var idRemove = int.Parse(Console.ReadLine() ?? string.Empty);
                        
                        var cursoRemove = cursos.Find(c => c.Id.Equals(idRemove));
                        
                        if (cursoRemove != null)
                        {
                            cursos.Remove(cursoRemove);
                            alunos.ForEach(aluno => aluno.Remover(cursoRemove.Id));
                            Console.WriteLine("Curso removido com sucesso!\n");
                        }
                        else
                        {
                            Console.WriteLine("Curso não encontrado.\n");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Exibir detalhes do Curso");
                        
                        Console.WriteLine("Digite o ID do curso que deseja saber mais:");
                        
                        var idDetalhe = int.Parse(Console.ReadLine() ?? string.Empty);
                        
                        var cursoDetalhe = cursos.Find(c => c.Id.Equals(idDetalhe));
                        
                        if (cursoDetalhe != null)
                        {
                            Console.WriteLine($"\nNome: {cursoDetalhe.Nome}");
                            Console.WriteLine($"Professor: {cursoDetalhe.Professor.Nome ?? "A definir"}");
                            var listaDetalhes = cursoDetalhe.Listar();
                            Console.WriteLine($"Alunos: {(listaDetalhes != string.Empty ?  "\n" + listaDetalhes 
                                : "A sala ainda está vazia!\n")}");
                        }
                        else
                        {
                            Console.WriteLine("Curso não encontrado.\n");
                        }
                        break;
                    case "9":
                        continuaCurso = false;
                        Console.WriteLine("Voltando...\n");
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.\n");
                        break;
                }
            } while (continuaCurso);
            break;
        case "9":
            continua = false;
            Console.WriteLine("\nAté mais!");
            break;
        default:
            Console.WriteLine("Opção inválida, tente novamente.");
            break;
    }
} while (continua);
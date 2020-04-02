# noverde
Projeto pessoal de Teste para Noverde em .Net 

# Abrir o projeto
Apos Clonar o repositório e ter instalado uma IDE para C#/.Net de sua preferência.
Opções de IDE: Visual Studio; Visual Code;
Abrir a solution em: \noverde\LoanWebApi\LoanApi\LoanApi.sln

# Executar/Start o Projeto
F5/ menu Debug > opção Start Debug

# Testar API do projeto

Exemplo de url para api de consulta:
GET https://localhost:44362/api/loan/1

Exemplo de url para api de inclusão:
Post https://localhost:44362/api/loan

body:
{
    "Name": "Teste  ",
    "Birthdate": "2007-08-24",
    "Cpf": "12345678901",
    "Amount": 1500.00,
    "Terms": 12,
    "Income": 100.00
}

# Executar Unit Test

1) Abrir View "Test Explorer": Menu View > opção Test Explorer
2) na view > 1º botão (Executar todos os testes / run all test/ Ctrl+ R, A) 

# SeguroAPI

Bem-vindo ao repositório do projeto desenvolvido com .NET 6. 
Este guia fornecerá as instruções necessárias para instalar e executar o projeto utilizando o Visual Studio Code.

## Pré-requisitos

Antes de começar, você precisa ter os seguintes itens instalados em seu sistema:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Extensão C# para Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

## Passo a Passo para Instalação

### 1. Clonar o Repositório

Abra o terminal e clone o repositório usando o comando abaixo:

```bash
git clone https://github.com/danielss2990/seguro.git

```

### 2. Navegar até o Diretório do Projeto
Após clonar o repositório, navegue até o diretório do projeto:
```bash
cd seguro
```

### 3. Restaurar as Dependências
Execute o seguinte comando para restaurar as dependências do projeto:

```bash
dotnet restore
```

### 4. Abrir o Projeto no Visual Studio Code
Para abrir o projeto no Visual Studio Code, utilize o comando:

```bash
code .
```

### 5. Compilar o Projeto
Para compilar o projeto, execute:

```bash
dotnet build
````

### 6. Executar o Projeto
Depois de uma compilação bem-sucedida, você pode executar o projeto com o seguinte comando:

```bash
dotnet run
```
A aplicação estará em execução, e você poderá acessá-lo em seu navegador ou por meio de ferramentas como Postman ou Insomnia.

## Estrutura do Projeto
O projeto contém as seguintes pastas e arquivos:

1. **Controllers/**: Contém os controladores da API.
2. **Models/**: Contém as classes de modelo.
3. **DTOs/**: Contém os Data Transfer Objects (DTOs) utilizados.
4. **Data/**: Contém a configuração do contexto do banco de dados.
5. **Program.cs**: Ponto de entrada do aplicativo.

## Contribuição
Se você deseja contribuir com este projeto, sinta-se à vontade para abrir um pull request ou relatar problemas.

## Licença
Este projeto está sob a Licença MIT.

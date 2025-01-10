<h1 align="center">Tech Challenge da Fase 1 - FIAP</h1>

## :computer: Projeto

Uma API para cadastro de contatos regionais, considerando a persistência de dados e qualidade de software.

O principal dado a ser validado neste projeto é o DDD do telefone de cada contato, onde inclusive uma das rotas retorna somente contatos de uma determinada região de acordo com DDD.

Para utilização, é necessário uma autenticação de usuário que é validada por um Enumerador de permissões, que em diferentes níveis permitirá determinadas ações.

Houve um trabalho para validação de dados que são fornecidos para cadastro, onde é verificado se os campos são preenchidos e se são válidos de acordo com cada tipo de dado.


## :wrench: Tecnologias Utilizadas

- **Documentação:** Swagger
- **ORM:** Dapper (SqlServer)
- **Autenticação:** JWTBearer e IdentityModel
- **Testes unitários:** xUnit e Moq
- **Logs:** ILog
- **Validação de dados:** Fluent Validation

## :arrow_forward: Como Utilizar

Neste projeto é utilizado banco de dadosSql Server. 
Após clonar o projeto, pode-se utilizar banco de dados local ou docker com a imagem e executar o script de banco de dados na raiz do projeto com o nome "scriptparacriarobanco.sql".

docker: ```docker compose up --build```

## Autores:
- [Daniel Lopes](https://github.com/daniel-lopes-98435)
- [Lucas Almeida](https://github.com/lugsdev)
- [Marla Amoury](https://github.com/marlamoury)
- [Rafael Sampaio](https://github.com/rafasampaio25)